using System;
using System.Collections.Generic;
using System.Linq;
using DeUrgenta.Domain.Api;
using DeUrgenta.RecurringJobs.Services.NotificationSenders;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DeUrgenta.RecurringJobs.Services;

public sealed class UserNotificationSendersFactory : IUserNotificationSendersFactory
{
    private readonly DeUrgentaContext _deUrgentaContext;
    private readonly IEnumerable<INotificationSender> _notificationSenders;
    private readonly ILogger<UserNotificationSendersFactory> _logger;

    public UserNotificationSendersFactory(
        DeUrgentaContext deUrgentaContext,
        IEnumerable<INotificationSender> notificationSenders,
        ILogger<UserNotificationSendersFactory> logger)
    {
        _deUrgentaContext = deUrgentaContext;
        _notificationSenders = notificationSenders;
        _logger = logger;
    }

    public async IAsyncEnumerable<INotificationSender> LoadSelectedSenders(Guid userId)
    {
        if (_notificationSenders == null || !_notificationSenders.Any())
        {
            _logger.LogWarning("No NotificationSenders configured. please revise the application settings");
            yield break;
        }
        
        var user = await _deUrgentaContext.Users
            .AsNoTracking()
            .Include(x => x.SelectedAlertChannels)
            .Where(x => x.Id == userId)
            .FirstOrDefaultAsync();

        if (user is null)
        {
            // TODO: some message?
            yield break;
        }

        if (!user.SelectedAlertChannels.Any())
        {
            // TODO: some message?
            yield break;
        }

        foreach (var selectedAlertChannel in user.SelectedAlertChannels)
        {
            yield return _notificationSenders.FirstOrDefault(x => x.Accept(selectedAlertChannel.Type));
        }
    }
}