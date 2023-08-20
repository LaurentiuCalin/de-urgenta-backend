using DeUrgenta.User.Api.Models.DTOs.Requests;
using FluentValidation;

namespace DeUrgenta.User.Api.Validators.RequestValidators;

public class DisableAlertChannelRequestValidator : AbstractValidator<DisableAlertChannelRequest>
{
    public DisableAlertChannelRequestValidator()
    {
        RuleFor(c => c.Type).NotEmpty().MinimumLength(1);
    }
}