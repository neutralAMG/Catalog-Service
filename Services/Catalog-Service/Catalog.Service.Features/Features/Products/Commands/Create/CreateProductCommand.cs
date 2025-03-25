using Catalog.Service.Features.Core;
using MediatR;

namespace Catalog.Service.Features.Features.Products.Commands.Create;

public sealed class CreateProductCommand : IRequest<Result<CreateProductResponse>>
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
}