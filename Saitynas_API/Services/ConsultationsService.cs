using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Saitynas_API.Services;

public interface IConsultationsService
{
    public void EnqueuePatient(string deviceToken);
}

public class ConsultationsService : IConsultationsService
{
    private readonly Queue<string> _patientsQueue;
    private readonly IApplePushNotificationService _apnService;
    
    public ConsultationsService(IApplePushNotificationService apnService)
    {
        _patientsQueue = new Queue<string>();
        _apnService = apnService;
    }
    
    public void EnqueuePatient(string deviceToken)
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
