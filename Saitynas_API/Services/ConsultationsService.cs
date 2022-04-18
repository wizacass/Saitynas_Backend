using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Saitynas_API.Models.Common;
using Saitynas_API.Models.Entities.Consultation;
using Saitynas_API.Models.Entities.Specialist;
using Saitynas_API.Repositories;

namespace Saitynas_API.Services;

public interface IConsultationsService
{
    public Task<Consultation> RequestConsultation(int patientId, string deviceToken, int? specialityId);
    public Task CancelConsultation(int consultationId, string deviceToken);
    public Task EndConsultation(int consultationId, string deviceToken);
    public Task StartConsultation(int consultationId, string deviceToken);
    public Task AcceptConsultation(int consultationId, string deviceToken, int specialistId);
    public Task EnqueueSpecialist(string deviceToken, int specialityId);
    public Task DequeueSpecialist(string deviceToken);
}

public class ConsultationsService : IConsultationsService
{
    private readonly Dictionary<int, Queue<string>> _patientsQueue;
    private readonly Dictionary<string, int> _specialistsQueue;

    private readonly IApplePushNotificationService _apnService;
    private readonly IServiceScopeFactory _scopeFactory;

    public ConsultationsService(
        IApplePushNotificationService apnService,
        IServiceScopeFactory serviceScopeFactory
    )
    {
        _patientsQueue = new Dictionary<int, Queue<string>> {{0, new Queue<string>()}};
        _specialistsQueue = new Dictionary<string, int>();

        _apnService = apnService;
        _scopeFactory = serviceScopeFactory;
    }

    public async Task<Consultation> RequestConsultation(int patientId, string deviceToken, int? specialityId)
    {
        using var scope = _scopeFactory.CreateScope();
        var repository = scope.ServiceProvider.GetService<IConsultationsRepository>()!;

        var consultation = new Consultation
        {
            PatientId = patientId,
            PatientDeviceToken = deviceToken,
            RequestedSpecialityId = specialityId
        };
        
        string specialistDeviceToken = FindAvailableSpecialist(specialityId);

        if (specialistDeviceToken == null)
        {
            EnqueuePatient(deviceToken, specialityId ?? 0);
        }
        else
        {
            consultation.SpecialistDeviceToken = specialistDeviceToken;
            await _apnService.PublishNotification(specialistDeviceToken, ApnMessage.SpecialistMessage).ConfigureAwait(false);
        }

        await repository.InsertAsync(consultation);

        return consultation;
    }

    private string FindAvailableSpecialist(int? requestedSpecialityId)
    {
        if (_specialistsQueue.Count == 0) return null;

        if (requestedSpecialityId == null) return _specialistsQueue.ElementAt(0).Key;
        
        return _specialistsQueue.FirstOrDefault(
            s => s.Value == requestedSpecialityId
        ).Key;
    }

    private void EnqueuePatient(string deviceToken, int specialityId)
    {
        if (!_patientsQueue.ContainsKey(specialityId))
        {
            _patientsQueue.Add(specialityId, new Queue<string>());
        }

        _patientsQueue[specialityId].Enqueue(deviceToken);
    }

    private async Task SendNotification(int specialityId)
    {
        const string message = "Consultation is about to start!";

        await Task.Factory.StartNew(() =>
            {
                Thread.Sleep(5000);

                if (_patientsQueue.Count <= 0) return;

                string token = _patientsQueue[specialityId].Dequeue();
                _apnService.PublishNotification(token, message);
            }
        );
    }

    public async Task CancelConsultation(int consultationId, string deviceToken)
    {
        using var scope = _scopeFactory.CreateScope();
        var repository = scope.ServiceProvider.GetService<IConsultationsRepository>()!;

        var consultation = await repository.GetAsync(consultationId);

        if (consultation.PatientDeviceToken != deviceToken)
        {
            throw new UnauthorizedAccessException();
        }

        consultation.IsCancelled = true;
        consultation.FinishedAt = DateTime.UtcNow;

        await repository.UpdateAsync(consultationId, consultation);

        DequeuePatient(consultation.PatientDeviceToken, consultation.RequestedSpecialityId);
    }

    private void DequeuePatient(string deviceToken, int? specialityId)
    {
        int key = specialityId ?? 0;

        var queue = new Queue<string>(_patientsQueue[key].Where(
            token => !string.Equals(token, deviceToken, StringComparison.Ordinal))
        );

        _patientsQueue[key] = queue;
    }

    public async Task EndConsultation(int consultationId, string deviceToken)
    {
        using var scope = _scopeFactory.CreateScope();
        var repository = scope.ServiceProvider.GetService<IConsultationsRepository>()!;
        var specialistsRepository = scope.ServiceProvider.GetService<ISpecialistsRepository>()!;

        var consultation = await repository.GetAsync(consultationId);

        if (consultation.PatientDeviceToken != deviceToken)
        {
            throw new UnauthorizedAccessException();
        }

        if (consultation.StartedAt == null)
        {
            throw new InvalidOperationException("consultation_not_started");
        }

        if (consultation.FinishedAt != null)
        {
            throw new InvalidOperationException("consultation_already_finished");
        }

        consultation.FinishedAt = DateTime.UtcNow;
        await repository.UpdateAsync(consultationId, consultation);

        int specialistId = (int) consultation.SpecialistId!;
        var specialist = await specialistsRepository.GetAsync(specialistId);
        specialist.SpecialistStatusId = SpecialistStatusId.Available;
        await specialistsRepository.UpdateAsync(specialistId, specialist);
    }

    public async Task StartConsultation(int consultationId, string deviceToken)
    {
        using var scope = _scopeFactory.CreateScope();
        var consultationsRepository = scope.ServiceProvider.GetService<IConsultationsRepository>()!;
        var specialistsRepository = scope.ServiceProvider.GetService<ISpecialistsRepository>()!;

        var consultation = await consultationsRepository.GetAsync(consultationId);

        if (consultation.PatientDeviceToken != deviceToken)
        {
            throw new UnauthorizedAccessException();
        }

        if (consultation.SpecialistId == null)
        {
            Console.WriteLine("No specialist in consultation!");
            throw new InvalidOperationException("consultation_not_ready");
        }

        if (consultation.StartedAt != null)
        {
            throw new InvalidOperationException("consultation_already_started");
        }
        
        consultation.StartedAt = DateTime.UtcNow;
        await consultationsRepository.UpdateAsync(consultationId, consultation);

        int specialistId = (int) consultation.SpecialistId!;
        var specialist = await specialistsRepository.GetAsync(specialistId);
        specialist.SpecialistStatusId = SpecialistStatusId.Available;
        await specialistsRepository.UpdateAsync(specialistId, specialist);
    }

    public async Task AcceptConsultation(int consultationId, string deviceToken, int specialistId)
    {
        using var scope = _scopeFactory.CreateScope();
        var consultationsRepository = scope.ServiceProvider.GetService<IConsultationsRepository>()!;

        var consultation = await consultationsRepository.GetAsync(consultationId);

        if (consultation.SpecialistDeviceToken != deviceToken)
        {
            throw new UnauthorizedAccessException();
        }

        consultation.SpecialistId = specialistId;
        await consultationsRepository.UpdateAsync(consultationId, consultation);

        DequeuePatient(consultation.PatientDeviceToken, consultation.RequestedSpecialityId);
        await _apnService.PublishNotification(consultation.PatientDeviceToken, ApnMessage.PatientMessage).ConfigureAwait(false);
    }

    public Task EnqueueSpecialist(string deviceToken, int specialityId)
    {
        _specialistsQueue.Add(deviceToken, specialityId);
        return Task.CompletedTask;
    }

    public Task DequeueSpecialist(string deviceToken)
    {
        _specialistsQueue.Remove(deviceToken);
        return Task.CompletedTask;
    }
}
