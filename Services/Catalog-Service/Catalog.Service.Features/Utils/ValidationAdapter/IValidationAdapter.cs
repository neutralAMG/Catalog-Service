

namespace Catalog.Service.Features.Utils.ValidationAdapter
{
    public interface IValidationAdapter<TypeIn, TypeOut>
    {
       static abstract  TypeOut AdaptValidationResult(TypeIn input);
    }
}
