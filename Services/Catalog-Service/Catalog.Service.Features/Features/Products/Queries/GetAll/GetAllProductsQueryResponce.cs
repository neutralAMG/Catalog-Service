using Catalog.Service.Domain.Entities;

namespace Catalog.Service.Features.Features.Products.Queries.GetAll
{
    public record GetAllProductsQueryResponce(List<ProductDTO> Products);

    public record ProductDTO(
        int Id,
        string Name,
        string Description,
        decimal Price,
        string PictureFileName,
        DateTimeOffset DateCreated)
    {
        public static implicit operator ProductDTO(Product product)
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