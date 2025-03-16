

namespace Catalog.Service.Features.Core
{
    public interface IInputValidator<TInput>
        where TInput : class
    {
        Result Validate(TInput input);
    }
}
