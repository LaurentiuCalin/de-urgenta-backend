using CSharpFunctionalExtensions;
using DeUrgenta.User.Api.Models.DTOs.Requests;
using FluentValidation.Results;
using MediatR;

namespace DeUrgenta.User.Api.Commands;

public class DisableAlertChannel : IRequest<Result<Unit, ValidationResult>>
{
    public string UserSub { get; }
    public DisableAlertChannelRequest DisableAlertChannelRequest { get; }

    public DisableAlertChannel(string userSub, DisableAlertChannelRequest disableAlertChannelRequest)
    {
        UserSub = userSub;
        DisableAlertChannelRequest = disableAlertChannelRequest;
    }
}