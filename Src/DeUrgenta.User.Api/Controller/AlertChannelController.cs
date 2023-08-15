using System.Collections.Generic;
using System.Collections.Immutable;
using System.Threading;
using System.Threading.Tasks;
using DeUrgenta.Common.Controllers;
using DeUrgenta.Common.Mappers;
using DeUrgenta.Common.Swagger;
using DeUrgenta.Domain.Api.Entities;
using DeUrgenta.User.Api.Commands;
using DeUrgenta.User.Api.Models;
using DeUrgenta.User.Api.Queries;
using DeUrgenta.User.Api.Swagger.AlertChannels;
using DeUrgenta.User.Api.Swagger.User;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;

namespace DeUrgenta.User.Api.Controller;

[Authorize]
[ApiController]
[Produces("application/json")]
[Consumes("application/json")]
[Route("alert-channel")]
public class AlertChannelController : BaseAuthController
{
    private readonly IMediator _mediator;
    private readonly IResultMapper _mapper;

    public AlertChannelController(IMediator mediator, IResultMapper mapper)
    {
        _mapper = mapper;
        _mediator = mediator;
    }

    /// <summary>
    /// Gets available alert channels
    /// </summary>
    [HttpGet("available")]
    [SwaggerResponse(StatusCodes.Status200OK, "Get available alert channels", typeof(IImmutableList<AlertChannelModel>))]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Something bad happened", typeof(ProblemDetails))]
    [SwaggerResponseExample(StatusCodes.Status200OK, typeof(GetAlertChannelsResponseExample))]
    [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ApplicationErrorResponseExample))]
    public async Task<ActionResult<IImmutableList<AlertChannelModel>>> GetAvailableAlertChannels(CancellationToken ct)
    {
        var result = await _mediator.Send(new GetAvailableAlertChannels(), ct);

        return Ok(result);
    }

    /// <summary>
    /// Gets selected alert channels for the user
    /// </summary>
    [HttpGet("selected")]
    [SwaggerResponse(StatusCodes.Status200OK, "Get selected alert channels for the user", typeof(IImmutableList<AlertChannelModel>))]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Something bad happened", typeof(ProblemDetails))]
    [SwaggerResponseExample(StatusCodes.Status200OK, typeof(GetAlertChannelsResponseExample))]
    [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ApplicationErrorResponseExample))]
    public async Task<ActionResult<IImmutableList<AlertChannelModel>>> GetSelectedAlertChannels(CancellationToken ct)
    {
        var result = await _mediator.Send(new GetUserAlertChannels(UserSub), ct);

        return await _mapper.MapToActionResult(result, ct);
    }

    /// <summary>
    /// Enables the alert channel for the user
    /// </summary>
    [HttpPost("enable")]
    [SwaggerResponse(StatusCodes.Status204NoContent, "Update successful")]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "A business rule was violated", typeof(ProblemDetails))]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Something bad happened", typeof(ProblemDetails))]
    [SwaggerRequestExample(typeof(KnownAlertChannelTypes), typeof(AddOrUpdateUserRequestExample))]
    [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(BusinessRuleViolationResponseExample))]
    [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ApplicationErrorResponseExample))]
    public async Task<ActionResult<UserModel>> UpdateUserAsync(UserRequest user, CancellationToken ct)
    {
        var result = await _mediator.Send(new UpdateUser(UserSub, user), ct);

        return await _mapper.MapToActionResult(result, ct);
    }

    /// <summary>
    /// Updates user info
    /// </summary>
    [HttpPost("disable")]
    [SwaggerResponse(StatusCodes.Status204NoContent, "Update successful")]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "A business rule was violated", typeof(ProblemDetails))]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Something bad happened", typeof(ProblemDetails))]
    [SwaggerRequestExample(typeof(UserRequest), typeof(AddOrUpdateUserRequestExample))]
    [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(BusinessRuleViolationResponseExample))]
    [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ApplicationErrorResponseExample))]
    public async Task<ActionResult<UserModel>> UpdateUserAsync(UserRequest user, CancellationToken ct)
    {
        var result = await _mediator.Send(new UpdateUser(UserSub, user), ct);

        return await _mapper.MapToActionResult(result, ct);
    }
}