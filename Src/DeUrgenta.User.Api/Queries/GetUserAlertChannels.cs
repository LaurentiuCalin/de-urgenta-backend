using System.Collections.Immutable;
using CSharpFunctionalExtensions;
using DeUrgenta.Common.Validation;
using DeUrgenta.User.Api.Models;
using MediatR;

namespace DeUrgenta.User.Api.Queries;

public class GetUserAlertChannels : IRequest<Result<IImmutableList<AlertChannelModel>, ValidationResult>>
{
    public string UserSub { get; }

    public GetUserAlertChannels(string userSub)
    {
        UserSub = userSub;
    }
}