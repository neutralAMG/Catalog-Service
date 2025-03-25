using Catalog.Service.Domain.Entities;

namespace Catalog.Service.Features.Features.Products.Commands.Create;

public record CreateProductResponse(int Id,
    string Name,
    string Description,
    decimal Price,
    string PictureFileName,
    DateTimeOffset DateCreated
)
{
    public static implicit operator CreateProductResponse(Product product)
    {
        return new(product.Id,
            product.Name,
            product.Description,
            product.Price,
            "",
            product.DateCreated);
    }
};