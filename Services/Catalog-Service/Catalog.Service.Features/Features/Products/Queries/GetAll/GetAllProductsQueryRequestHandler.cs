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
        private readonly ILogger<GetAllProductsQueryRequest> _logger;

        public GetAllProductsQueryRequestHandler(
            IRepository<Product, int> productRepository, 
            IInputValidator<GetAllProductsQueryRequest> validator, 
            ILogger<GetAllProductsQueryRequest> logger)
        {
            _productRepository = productRepository;
            _validator = validator;
            _logger = logger;
        }
        public async Task<Result<GetAllProductsQueryResponce>> Handle(GetAllProductsQueryRequest request, CancellationToken cancellationToken)
        {
            Result result = _validator.ValidateInput(request);

            if (result.IsFailure)
            {
                _logger.LogWarning("Validation failed for request {@request}", request);
                return result;
            }

            List<Product>? products = await _productRepository.GetAllAsync(null, cancellationToken);

            if(products == null)
            {
                _logger.LogWarning("No products found");
                return Result.Failure("No products found");
            }

            return Result<GetAllProductsQueryResponce>.Success("",  new GetAllProductsQueryResponce(products.Select(p => (ProductDTO)p).ToList()));
        }
    }
}
