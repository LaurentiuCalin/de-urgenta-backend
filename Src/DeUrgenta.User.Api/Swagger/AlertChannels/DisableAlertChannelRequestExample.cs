using DeUrgenta.User.Api.Models.DTOs.Requests;
using Swashbuckle.AspNetCore.Filters;

namespace DeUrgenta.User.Api.Swagger.AlertChannels;

public class DisableAlertChannelRequestExample : IExamplesProvider<DisableAlertChannelRequest>
{
    public DisableAlertChannelRequest GetExamples()
    {
        return new DisableAlertChannelRequest("email");
    }
}