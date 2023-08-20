using System.Threading;
using System.Threading.Tasks;
using DeUrgenta.Common.Validation;
using DeUrgenta.Domain.Api;
using DeUrgenta.User.Api.Commands;
using Microsoft.EntityFrameworkCore;

namespace DeUrgenta.User.Api.Validators;

public class EnableAlertChannelValidator : IValidateRequest<EnableAlertChannel>
{
    private readonly DeUrgentaContext _context;
    
    public EnableAlertChannelValidator(DeUrgentaContext context)
    {
        _context = context;
    }

    public async Task<ValidationResult> IsValidAsync(EnableAlertChannel request, CancellationToken ct)
    {
        var userExists = await _context.Users.AnyAsync(u => u.Sub == request.UserSub, ct);

        if (!userExists)
        {
            return ValidationResult.GenericValidationError;
        }

        var alertChannelExists = await _context.AlertChannels.AnyAsync(x => x.Type == request.EnableAlertChannelRequest.Type, cancellationToken: ct);
        
        return !alertChannelExists ? ValidationResult.GenericValidationError : ValidationResult.Ok;
    }
}