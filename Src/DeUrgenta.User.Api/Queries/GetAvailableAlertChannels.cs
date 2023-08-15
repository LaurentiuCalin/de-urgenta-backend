using System.Collections.Immutable;
using DeUrgenta.User.Api.Models;
using MediatR;

namespace DeUrgenta.User.Api.Queries;

public class GetAvailableAlertChannels :  IRequest<IImmutableList<AlertChannelModel>>
{
    
}