using Application.CQRS.Products.Commands.Requests;
using FluentValidation;

namespace Application.CQRS.Products.Validator;

public class CreateProductValidator : AbstractValidator<CreateProductRequest>
{
    public CreateProductValidator()
    {
        RuleFor(p => p.Name).NotEmpty().MaximumLength(255);
        RuleFor(p => p.Price).GreaterThan(0).NotNull();
        RuleFor(p => p.Stock).GreaterThanOrEqualTo(0).NotNull();
        RuleFor(p => p.CategoryId).NotNull();
        RuleFor(p => p.Description).NotEmpty().MaximumLength(255);
    }
}
