using CSharpFunctionalExtensions;
using DeUrgenta.User.Api.Models.DTOs.Requests;
using FluentValidation.Results;
using MediatR;

namespace DeUrgenta.User.Api.Commands;

public class DisableAlertChannel : IRequest<Result<Unit, ValidationResult>>
{
    public string UserSub { get; }
    public DisableAlertChannelRequest Type { get; }

    public DisableAlertChannel(string userSub, DisableAlertChannelRequest type)
    {
        UserSub = userSub;
        Type = type;
    }
}