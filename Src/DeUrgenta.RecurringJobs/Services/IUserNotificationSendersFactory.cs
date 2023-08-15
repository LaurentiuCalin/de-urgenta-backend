using System;
using System.Collections.Generic;
using DeUrgenta.RecurringJobs.Services.NotificationSenders;

namespace DeUrgenta.RecurringJobs.Services;

public interface IUserNotificationSendersFactory
{
    IAsyncEnumerable<INotificationSender> LoadSelectedSenders(Guid userId);
}