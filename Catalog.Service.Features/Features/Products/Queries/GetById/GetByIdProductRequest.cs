using Catalog.Service.Features.Core;
using MediatR;

namespace Catalog.Service.Features.Features.Products.Queries.GetById
{
    public class GetByIdProductRequest : IRequest<Result<GetByIdProductResponce>>
    {
        public int ProductId { get; set; }  
    }
}
