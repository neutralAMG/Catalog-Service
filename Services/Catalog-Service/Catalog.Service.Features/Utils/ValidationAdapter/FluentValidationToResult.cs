

using Catalog.Service.Features.Core;
using FluentValidation.Results;

namespace Catalog.Service.Features.Utils.ValidationAdapter
{
    public class FluentValidationToResult : IValidationAdapter<ValidationResult, Result>
    {
        public Result AdaptValidationReult(ValidationResult input)
        {
         return !input.IsValid 
                ? Result.Failure("There was som validations errors: "+ string.Join(" ", input.Errors.Select(e => e.ErrorMessage)))
                : Result.Success("Validation passed");
        }
    }
}
