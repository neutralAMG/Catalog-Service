
using Catalog.Service.Domain.Entities;

namespace Catalog.Service.Features.Features.Products.Queries.GetById
{
    public record GetByIdProductResponce(int Id,
        string Name,
        string Description,
        decimal Price,
        string PictureFileName,
        DateTimeOffset DateCreated
        )
    {
        public static implicit operator GetByIdProductResponce(Product product)
        {
            return new(product.Id,
                product.Name,
                product.Description,
                product.Price,
                "",
                product.DateCreated);
        }
    };
}
