
using Catalog.Service.Features.Core;
using Catalog.Service.Features.Utils.ValidationAdapter;
using FluentValidation;
using System.Data;

namespace Catalog.Service.Features.Features.Products.Queries.GetById
{
    public class GetByIdProductValidator :AbstractValidator<GetByIdProductRequest>, IInputValidator<GetByIdProductRequest>
    {
        private readonly FluentValidationToResult _validationAdapter;

        public GetByIdProductValidator(FluentValidationToResult validationAdapter)
        {
           _validationAdapter = validationAdapter;

            RuleFor(r => r)
                .NotNull().WithMessage("The input cant be null");
            
            RuleFor(r => r.ProductId)
                .NotNull()
                .WithMessage("The id cant be null")
                .LessThanOrEqualTo(0)
                .WithMessage("The user identification was in an incorrect format");
        }
        public Result ValidateInput(GetByIdProductRequest input)
        {
            FluentValidation.Results.ValidationResult result = base.Validate(input);
            return _validationAdapter.AdaptValidationReult(result);
        }
    }
}
