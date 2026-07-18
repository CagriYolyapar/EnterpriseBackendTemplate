using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace EnterpriseBackendTemplate.Application.Features.Products.Update;

public sealed class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
	public UpdateProductCommandValidator()
	{
        RuleFor(command => command.Id)
           .NotEmpty()
           .WithMessage("Product id is required.");

        RuleFor(command => command.Name)
            .NotEmpty()
            .WithMessage("Product name is required.")
            .MaximumLength(200)
            .WithMessage(
                "Product name must not exceed 200 characters.");

        RuleFor(command => command.Price)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Product price cannot be negative.");
    }
}
