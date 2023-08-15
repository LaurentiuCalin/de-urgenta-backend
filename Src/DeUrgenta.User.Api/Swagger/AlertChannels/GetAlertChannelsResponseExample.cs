using System.Collections.Generic;
using DeUrgenta.User.Api.Models;
using Swashbuckle.AspNetCore.Filters;

namespace DeUrgenta.User.Api.Swagger.AlertChannels;

public class GetAlertChannelsResponseExample : IExamplesProvider<List<AlertChannelModel>>
{
    public List<AlertChannelModel> GetExamples()
    {
        return new List<AlertChannelModel>
        {
            new(1, "email"),
            new(2, "sms")
        };
    }
}