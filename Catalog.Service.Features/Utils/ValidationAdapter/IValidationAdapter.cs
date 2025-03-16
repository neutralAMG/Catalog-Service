

namespace Catalog.Service.Features.Utils.ValidationAdapter
{
    public interface IValidationAdapter<TypeIn, TypeOut>
    {
        TypeOut AdaptValidationReult(TypeIn input);
    }
}
