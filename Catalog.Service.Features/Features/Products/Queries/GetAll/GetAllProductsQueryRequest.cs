

using Catalog.Service.Features.Core;
using MediatR;

namespace Catalog.Service.Features.Features.Products.Queries.GetAll
{
    public class GetAllProductsQueryRequest : IRequest<Result<List<GetAllProductsQueryResponce>>>
    {
    }
}
