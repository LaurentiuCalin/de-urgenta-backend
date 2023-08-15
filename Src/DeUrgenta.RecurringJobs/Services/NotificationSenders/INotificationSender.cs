using System.Threading.Tasks;
using DeUrgenta.Domain.RecurringJobs.Entities;

namespace DeUrgenta.RecurringJobs.Services.NotificationSenders
{
    public interface INotificationSender
    {
        bool Accept(string alertChannelType);
        Task<bool> SendNotificationAsync(Notification notification);
    }
}