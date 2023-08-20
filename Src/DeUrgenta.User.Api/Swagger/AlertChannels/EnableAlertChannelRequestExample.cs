using DeUrgenta.User.Api.Models.DTOs.Requests;
using Swashbuckle.AspNetCore.Filters;

namespace DeUrgenta.User.Api.Swagger.AlertChannels;

public class EnableAlertChannelRequestExample : IExamplesProvider<EnableAlertChannelRequest>
{
    public EnableAlertChannelRequest GetExamples()
    {
        return new EnableAlertChannelRequest("email");
    }
}