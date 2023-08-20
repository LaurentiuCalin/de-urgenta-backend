using DeUrgenta.User.Api.Models.DTOs.Requests;
using FluentValidation;

namespace DeUrgenta.User.Api.Validators.RequestValidators;

public class EnableAlertChannelRequestValidator : AbstractValidator<EnableAlertChannelRequest>
{
    public EnableAlertChannelRequestValidator()
    {
        RuleFor(c => c.Type).NotEmpty().MinimumLength(1);
    }
}