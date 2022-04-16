using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Saitynas_API.Models.Entities.Consultation;
using Saitynas_API.Repositories;

namespace Saitynas_API.Services;

public interface IConsultationsService
{
    public Task<Consultation> RequestConsultation(int patientId, string deviceToken, int? specialityId);
    public Task CancelConsultation(int consultationId, string deviceToken);
    public Task EndConsultation(int consultationId, string deviceToken);
    public Task StartConsultation(int consultationId, string deviceToken);
}

public class ConsultationsService : IConsultationsService
{
    private readonly Dictionary<int, Queue<string>> _patientsQueue;

    private readonly IApplePushNotificationService _apnService;
    private readonly IServiceScopeFactory _scopeFactory;

    public ConsultationsService(
        IApplePushNotificationService apnService,
        IServiceScopeFactory serviceScopeFactory
    )
    {
        _patientsQueue = new Dictionary<int, Queue<string>> {{ 0, new Queue<string>() }};

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

        await repository.InsertAsync(consultation);

        EnqueuePatient(deviceToken, specialityId ?? 0);

        return consultation;
    }
    
    private void EnqueuePatient(string deviceToken, int specialityId)
    {
        if (!_patientsQueue.ContainsKey(specialityId))
        {
            _patientsQueue.Add(specialityId, new Queue<string>());
        }

        _patientsQueue[specialityId].Enqueue(deviceToken);
        
        var _ = SendNotification(specialityId);
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

        var consultation = await repository.GetAsync(consultationId);

        if (consultation.PatientDeviceToken != deviceToken)
        {
            throw new UnauthorizedAccessException();
        }

        consultation.FinishedAt = DateTime.UtcNow;
        // TODO: Set specialist back to available again
        
        await repository.UpdateAsync(consultationId, consultation);
    }

    public async Task StartConsultation(int consultationId, string deviceToken)
    {
        using var scope = _scopeFactory.CreateScope();
        var repository = scope.ServiceProvider.GetService<IConsultationsRepository>()!;

        var consultation = await repository.GetAsync(consultationId);

        if (consultation.PatientDeviceToken != deviceToken)
        {
            throw new UnauthorizedAccessException();
        }

        consultation.StartedAt = DateTime.UtcNow;
        // TODO: Set specialist back to busy
        
        await repository.UpdateAsync(consultationId, consultation);
    }
}
