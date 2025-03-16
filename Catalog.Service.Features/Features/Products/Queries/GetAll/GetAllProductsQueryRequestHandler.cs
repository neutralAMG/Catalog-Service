

using Catalog.Service.Domain.Entities;
using Catalog.Service.Domain.Repository;
using Catalog.Service.Features.Core;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Catalog.Service.Features.Features.Products.Queries.GetAll
{
    public class GetAllProductsQueryRequestHandler : IRequestHandler<GetAllProductsQueryRequest, Result<GetAllProductsQueryResponce>>
    {
        private readonly IRepository<Product, int> _productRepository;
        private readonly IInputValidator<GetAllProductsQueryRequest> _validator;
        private readonly ILogger<GetAllProductsQueryResponce> _logger;

        public GetAllProductsQueryRequestHandler(
            IRepository<Product, int> productRepository, 
            IInputValidator<GetAllProductsQueryRequest> validator, 
            ILogger<GetAllProductsQueryResponce> logger)
        {
            _productRepository = productRepository;
            _validator = validator;
            _logger = logger;
        }
        public Task<Result<GetAllProductsQueryResponce>> Handle(GetAllProductsQueryRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
