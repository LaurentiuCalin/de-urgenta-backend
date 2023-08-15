using System.Threading.Tasks;
using DeUrgenta.Domain.Api.Entities;
using DeUrgenta.Emailing.Service.Senders;
using DeUrgenta.Domain.RecurringJobs;
using DeUrgenta.Domain.RecurringJobs.Entities;
using DeUrgenta.RecurringJobs.Services.NotificationSenders.EmailBuilders;

namespace DeUrgenta.RecurringJobs.Services.NotificationSenders
{
    public class EmailNotificationSender : INotificationSender
    {
        private readonly JobsContext _jobsContext;
        private readonly IEmailSender _emailSender;
        private readonly EmailRequestBuilderFactory _factory;

        public EmailNotificationSender(JobsContext jobsContext, IEmailSender emailSender, EmailRequestBuilderFactory factory)
        {
            _jobsContext = jobsContext;
            _emailSender = emailSender;
            _factory = factory;
        }

        public bool Accept(string alertChannelType)
        {
            return KnownAlertChannelTypes.Email == alertChannelType;
        }

        public async Task<bool> SendNotificationAsync(Notification notification)
        {
            if (notification == null)
            {
                return false;
            }

            await _jobsContext.Entry(notification).Reference(x => x.CertificationDetails).LoadAsync();
            await _jobsContext.Entry(notification).Reference(x => x.ItemDetails).LoadAsync();

            var emailRequestBuilder = _factory.GetBuilderInstance(notification.Type);

            var email = await emailRequestBuilder.CreateEmailRequest(notification);
            return await _emailSender.SendAsync(email);
        }
    }
}