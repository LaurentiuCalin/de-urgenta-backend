using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeUrgenta.Domain.RecurringJobs.Entities;

namespace DeUrgenta.RecurringJobs.Services
{
    public class NotificationService : INotificationService
    {
        private readonly UserNotificationSendersFactory _userNotificationSendersFactory;

        public NotificationService(UserNotificationSendersFactory userNotificationSendersFactory)
        {
            _userNotificationSendersFactory = userNotificationSendersFactory;
        }

        public async Task<NotificationStatus> SendNotificationAsync(Notification notification)
        {
            var sendingTasks = new List<Task<bool>>();
            await foreach (var notificationSender in _userNotificationSendersFactory.LoadSelectedSenders(notification.UserId))
            {
                sendingTasks.Add(notificationSender.SendNotificationAsync(notification));
            }
            
            var sentSuccessful = await Task.WhenAll(sendingTasks);

            if (!sentSuccessful.Any())
            {
                return NotificationStatus.NotSent;
            }
            
            var notificationStatus = sentSuccessful.All(b => b)
                ? NotificationStatus.Sent
                : (sentSuccessful.Any(b => b)
                    ? NotificationStatus.PartlySent
                    : NotificationStatus.ErrorSending);
            return notificationStatus;
        }
    }
}