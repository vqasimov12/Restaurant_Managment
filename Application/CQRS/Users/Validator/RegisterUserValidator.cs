using FluentValidation;
using static Application.CQRS.Users.Handlers.Register;

namespace Application.CQRS.Users.Validator;

public class RegisterUserValidator:AbstractValidator<Command>
{
    public RegisterUserValidator()
    {
        RuleFor(x => x.Email).NotEmpty().EmailAddress().MaximumLength(50);
        RuleFor(x => x.Name).NotEmpty().MaximumLength(50);
        RuleFor(x => x.Surname).NotEmpty().MaximumLength(50);
        RuleFor(x => x.Phone).NotEmpty().MaximumLength(20).Matches(@"^\+994(5[015]|7[07])\d{7}$");

    }
}
