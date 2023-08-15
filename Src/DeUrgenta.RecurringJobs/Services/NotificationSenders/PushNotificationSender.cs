using System;
using System.Threading.Tasks;
using DeUrgenta.Domain.Api.Entities;
using DeUrgenta.Domain.RecurringJobs.Entities;

namespace DeUrgenta.RecurringJobs.Services.NotificationSenders
{
    public class PushNotificationSender : INotificationSender
    {
        public bool Accept(string alertChannelType)
        {
            return KnownAlertChannelTypes.PushNotification == alertChannelType;
        }
        
        public Task<bool> SendNotificationAsync(Notification notification)
        {
            throw new NotImplementedException();
        }
    }
}