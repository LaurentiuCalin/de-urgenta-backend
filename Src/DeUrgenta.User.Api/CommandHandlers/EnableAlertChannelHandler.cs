using System.Threading;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using DeUrgenta.Common.Validation;
using DeUrgenta.Domain.Api;
using DeUrgenta.User.Api.Commands;
using MediatR;
using ValidationResult = FluentValidation.Results.ValidationResult;

namespace DeUrgenta.User.Api.CommandHandlers;

public class EnableAlertChannelHandler : IRequestHandler<EnableAlertChannel, Result<Unit, ValidationResult>>
{
    private readonly IValidateRequest<EnableAlertChannel> _validator;
    private readonly DeUrgentaContext _context;

    public EnableAlertChannelHandler(IValidateRequest<EnableAlertChannel> validator, DeUrgentaContext context)
    {
        _validator = validator;
        _context = context;
    }

    public Task<Result<Unit, ValidationResult>> Handle(EnableAlertChannel request, CancellationToken cancellationToken)
    {
        throw new System.NotImplementedException();
    }
}