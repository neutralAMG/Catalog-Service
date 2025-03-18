

using Catalog.Service.Domain.Entities;

namespace Catalog.Service.Features.Features.Products.Queries.GetAll
{
    public record GetAllProductsQueryResponce(
        int Id, 
        string Name, 
        string Description, 
        decimal Price, 
        string PictureFileName,
        DateTimeOffset DateCreated)
    {
        public static implicit operator GetAllProductsQueryResponce(Product product) {
            return new(product.Id,
                product.Name,
                product.Description,
                product.Price,
                "",
                product.DateCreated);
        }
    };
}
