using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Saitynas_API.Models.Entities.Consultation;
using Saitynas_API.Models.Entities.Patient;
using Saitynas_API.Repositories;

namespace Saitynas_API.Services;

public interface IConsultationsService
{
    public Task RequestConsultation(int patientId, string deviceToken);
}

public class ConsultationsService : IConsultationsService
{
    private readonly Queue<string> _patientsQueue;

    private readonly IApplePushNotificationService _apnService;
    private readonly IServiceScopeFactory _scopeFactory;

    public ConsultationsService(
        IApplePushNotificationService apnService,
        IServiceScopeFactory serviceScopeFactory
    )
    {
        _patientsQueue = new Queue<string>();
        _apnService = apnService;
        _scopeFactory = serviceScopeFactory;
    }

    public async Task RequestConsultation(int patientId, string deviceToken)
    {
        using var scope = _scopeFactory.CreateScope();
        var repository = scope.ServiceProvider.GetService<IConsultationsRepository>()!;

        var consultation = new Consultation
        {
            PatientId = patientId,
            PatientDeviceToken = deviceToken
        };

        await repository.InsertAsync(consultation);

        EnqueuePatient(deviceToken);
    }

    private void EnqueuePatient(string deviceToken)
    {
        _patientsQueue.Enqueue(deviceToken);

        var _ = SendNotification();
    }

    private async Task SendNotification()
    {
        const string message = "Consultation is about to start!";

        await Task.Factory.StartNew(() =>
            {
                Thread.Sleep(5000);

                if (_patientsQueue.Count <= 0) return;

                string token = _patientsQueue.Dequeue();
                _apnService.PublishNotification(token, message);
            }
        );
    }
}
