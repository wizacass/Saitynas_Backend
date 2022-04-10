using System.Net.Http;
using System.Threading.Tasks;
using CorePush.Apple;

namespace Saitynas_API.Services;

public interface IApplePushNotificationService
{ 
    Task PublishNotification(string deviceToken, string message);
}

public class ApplePushNotificationService : IApplePushNotificationService
{
    private readonly ApnSender _sender;

    public ApplePushNotificationService(ApnSettings settings, HttpClient httpClient)
    {
        _sender = new ApnSender(settings, httpClient);
    }

    public async Task PublishNotification(string deviceToken, string message)
    {
        var notification = new 
        {
            aps = new
            {
                alert = message,
                sound = "default"
            }
        };
        
        await _sender.SendAsync(notification, deviceToken);
    }
}
