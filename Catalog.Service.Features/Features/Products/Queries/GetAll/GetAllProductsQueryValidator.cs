

using Catalog.Service.Features.Core;
using FluentValidation;

namespace Catalog.Service.Features.Features.Products.Queries.GetAll
{
    public class GetAllProductsQueryValidator :AbstractValidator<GetAllProductsQueryRequest>, IInputValidator<GetAllProductsQueryRequest>
    {
        public GetAllProductsQueryValidator()
        {
            RuleFor(x => x).NotNull();
        }
        public Result Validate(GetAllProductsQueryRequest input)
        {
            var result =  base.Validate(input);
            return result.IsValid ? Result.Success("Success") : Result.Failure("there was some validation Mismatches: " + string.Join(" ", result.Errors.Select(e => e.ErrorMessage)));
        }
    }
}
