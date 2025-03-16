

namespace Catalog.Service.Features.Core
{
    public interface IInputValidator<TInput>
        where TInput : class
    {
        Result ValidateInput(TInput input);

        //TODO: Implement async validation

        //  Task<Result> ValidateInputAsync(TInput input);
    }

}
