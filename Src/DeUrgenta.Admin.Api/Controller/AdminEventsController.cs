using System;
using System.Threading.Tasks;
using DeUrgenta.Admin.Api.Commands;
using DeUrgenta.Admin.Api.Models;
using DeUrgenta.Admin.Api.Queries;
using DeUrgenta.Admin.Api.Swagger.Events;
using DeUrgenta.Common.Mappers;
using DeUrgenta.Common.Models.Events;
using DeUrgenta.Common.Models.Pagination;
using DeUrgenta.Common.Swagger;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;

namespace DeUrgenta.Admin.Api.Controller
{
    [ApiController]
    [Authorize]
    [Produces("application/json")]
    [Consumes("application/json")]
    [Route("admin/event")]
    public class AdminEventsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IResultMapper _mapper;

        public AdminEventsController(IMediator mediator, IResultMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        /// <summary>
        /// Get all events events
        /// </summary>
        /// <returns></returns>
        [HttpGet("/admin/events")]
        [SwaggerResponse(StatusCodes.Status200OK, "Events", typeof(PagedResult<EventResponseModel>))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Something bad happened", typeof(ProblemDetails))]

        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(GetEventsResponseExample))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ApplicationErrorResponseExample))]
        public async Task<ActionResult<PagedResult<EventResponseModel>>> GetEventsAsync([FromQuery] PaginationQueryModel pagination)
        {
            var query = new GetEvents(pagination);
            var result = await _mediator.Send(query);

            return await _mapper.MapToActionResult(result);
        }

        /// <summary>
        /// Adds a new event
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [SwaggerResponse(StatusCodes.Status200OK, "New event", typeof(EventResponseModel))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "A business rule was violated", typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Something bad happened", typeof(ProblemDetails))]

        [SwaggerRequestExample(typeof(EventRequest), typeof(AddOrUpdateEventRequestExample))]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(AddOrUpdateEventResponseExample))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(BusinessRuleViolationResponseExample))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ApplicationErrorResponseExample))]
        public async Task<ActionResult<EventResponseModel>> CreateNewEventAsync([FromBody] EventRequest eventModel)
        {
            var command = new CreateEvent(eventModel);
            var result = await _mediator.Send(command);

            return await _mapper.MapToActionResult(result);
        }

        /// <summary>
        /// Updates a event
        /// </summary>
        [HttpPut]
        [Route("{eventId:guid}")]

        [SwaggerResponse(StatusCodes.Status200OK, "Updated event", typeof(EventResponseModel))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "A business rule was violated", typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Something bad happened", typeof(ProblemDetails))]

        [SwaggerRequestExample(typeof(EventRequest), typeof(AddOrUpdateEventRequestExample))]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(AddOrUpdateEventResponseExample))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(BusinessRuleViolationResponseExample))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ApplicationErrorResponseExample))]
        public async Task<ActionResult<EventResponseModel>> UpdateEventAsync([FromRoute] Guid eventId, [FromBody] EventRequest eventModel)
        {
            var command = new UpdateEvent(eventId, eventModel);
            var result = await _mediator.Send(command);

            return await _mapper.MapToActionResult(result);
        }

        /// <summary>
        /// Delete a event
        /// </summary>
        [HttpDelete]
        [Route("{eventId:guid}")]

        [SwaggerResponse(StatusCodes.Status204NoContent, "Event was deleted")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "A business rule was violated", typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Something bad happened", typeof(ProblemDetails))]

        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(BusinessRuleViolationResponseExample))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ApplicationErrorResponseExample))]
        public async Task<IActionResult> DeleteEventAsync([FromRoute] Guid eventId)
        {
            var command = new DeleteEvent(eventId);
            var result = await _mediator.Send(command);

            return await _mapper.MapToActionResult(result);
        }
    }
}
