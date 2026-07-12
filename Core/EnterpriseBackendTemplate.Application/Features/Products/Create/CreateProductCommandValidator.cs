using FluentValidation;

namespace EnterpriseBackendTemplate.Application.Features.Products.Create;

public sealed class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(command => command.Name).
            NotEmpty().
            WithMessage("Product name is required.").
            MaximumLength(200).
            WithMessage("Product name must not exceed 200 characters");

        RuleFor(command => command.Price)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Product price cannot be negative");
    }
}

