

using Catalog.Service.Features.Core;
using FluentValidation.Results;

namespace Catalog.Service.Features.Utils.ValidationAdapter
{
    internal sealed class FluentValidationToResult : IValidationAdapter<ValidationResult, Result>
    {
        public static Result AdaptValidationResult(ValidationResult input)
        {
         return !input.IsValid 
                ? Result.Failure("There was som validations errors: "+ string.Join(" ", input.Errors.Select(e => e.ErrorMessage)))
                : Result.Success("Validation passed");
        }
    }
}
