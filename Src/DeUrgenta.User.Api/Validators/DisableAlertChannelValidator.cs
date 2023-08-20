using System.Threading;
using System.Threading.Tasks;
using DeUrgenta.Common.Validation;
using DeUrgenta.Domain.Api;
using DeUrgenta.User.Api.Commands;
using Microsoft.EntityFrameworkCore;

namespace DeUrgenta.User.Api.Validators;

public class DisableAlertChannelValidator : IValidateRequest<DisableAlertChannel>
{
    private readonly DeUrgentaContext _context;
    
    public DisableAlertChannelValidator(DeUrgentaContext context)
    {
        _context = context;
    }

    public async Task<ValidationResult> IsValidAsync(DisableAlertChannel request, CancellationToken ct)
    {
        var userExists = await _context.Users.AnyAsync(u => u.Sub == request.UserSub, ct);

        return userExists ? ValidationResult.Ok : ValidationResult.GenericValidationError;
    }
}