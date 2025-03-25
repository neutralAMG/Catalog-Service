using System.Data;
using Catalog.Service.Features.Core;
using Catalog.Service.Features.Utils.ValidationAdapter;
using FluentValidation;

namespace Catalog.Service.Features.Features.Products.Commands.Create;

public class CreateProductValidator : AbstractValidator<CreateProductCommand>, IInputValidator<CreateProductCommand>
{


    public CreateProductValidator()
    {
    
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("The ame is required");
        
        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("The Description is required.");
        RuleFor(x => x.Price)
            .NotEmpty().WithMessage("The price cant be empty")
            .GreaterThanOrEqualTo(1)
            .WithMessage("The price must be greater than or equal to 1");
        
    }

    public Result ValidateInput(CreateProductCommand input)
    {
      FluentValidation.Results.ValidationResult result = base.Validate(input);
      return FluentValidationToResult.AdaptValidationResult(result);
    }
}