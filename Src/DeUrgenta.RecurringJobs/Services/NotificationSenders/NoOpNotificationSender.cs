using System.Threading.Tasks;
using DeUrgenta.Domain.RecurringJobs.Entities;
using Microsoft.Extensions.Logging;

namespace DeUrgenta.RecurringJobs.Services.NotificationSenders
{
    public class NoOpNotificationSender : INotificationSender
    {
        private readonly ILogger<NoOpNotificationSender> _logger;

        public NoOpNotificationSender(ILogger<NoOpNotificationSender> logger)
        {
            _logger = logger;
        }

        public bool Accept(string alertChannelType)
        {
            return false;
        }

        public Task<bool> SendNotificationAsync(Notification notification)
        {
            _logger.LogInformation($"Sent notification Id '{notification}' to user.");

            return Task.FromResult(true);
        }
    }
}