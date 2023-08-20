using CSharpFunctionalExtensions;
using DeUrgenta.User.Api.Models.DTOs.Requests;
using FluentValidation.Results;
using MediatR;

namespace DeUrgenta.User.Api.Commands;

public class EnableAlertChannel : IRequest<Result<Unit, ValidationResult>>
{
    public string UserSub { get; }
    public EnableAlertChannelRequest Type { get; }

    public EnableAlertChannel(string userSub, EnableAlertChannelRequest type)
    {
        UserSub = userSub;
        Type = type;
    }
}