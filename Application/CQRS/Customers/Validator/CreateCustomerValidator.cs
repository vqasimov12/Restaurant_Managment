using Application.CQRS.Categories.Commands.Requests;
using Application.CQRS.Customers.Commands.Requests;
using FluentValidation;

namespace Application.CQRS.Customers.Validator;

public class CreateCustomerValidator : AbstractValidator<CreateCustomerRequest>
{
    public CreateCustomerValidator()
    {
        RuleFor(c => c.Name).NotEmpty().MaximumLength(25);
        RuleFor(z => z.Surname).NotEmpty().MaximumLength(25);
        RuleFor(z => z.Address).NotEmpty().MaximumLength(255);
        RuleFor(z => z.Phone)
           .NotEmpty()
           .Matches(@"^\+994(50|51|70|77|10|99|55|57)\d{7}$|^\+994 (50|51|70|77|10|99|55|57) \d{3} \d{2} \d{2}$")
           .WithMessage("Phone number must start with '+994', have a valid prefix (50, 51, 70, 77, 10, 99, 55, 57), and follow either format: '+994XXXXXXXXX' or '+994 XX XXX XX XX'.");
        RuleFor(z => z.Email).NotEmpty().EmailAddress();
    }
}
