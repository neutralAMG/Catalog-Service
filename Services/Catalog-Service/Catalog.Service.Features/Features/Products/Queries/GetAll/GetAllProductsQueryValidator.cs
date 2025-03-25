

using Catalog.Service.Features.Core;
using Catalog.Service.Features.Utils.ValidationAdapter;
using FluentValidation;

namespace Catalog.Service.Features.Features.Products.Queries.GetAll
{
    public class GetAllProductsQueryValidator :AbstractValidator<GetAllProductsQueryRequest>, IInputValidator<GetAllProductsQueryRequest>
    {
        public GetAllProductsQueryValidator()
        {
            RuleFor(x => x)
                .NotNull().WithMessage("The request cant be empty");
        }

        public Result ValidateInput(GetAllProductsQueryRequest input)
        {
            var result =  base.Validate(input);
            return FluentValidationToResult.AdaptValidationResult(result);
        }
    }
}
