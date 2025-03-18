using Catalog.Service.Domain.Entities;
using Catalog.Service.Domain.Repository;
using Catalog.Service.Features.Core;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Catalog.Service.Features.Features.Products.Queries.GetById
{
    public class GetByIdProductRequestHandler : IRequestHandler<GetByIdProductRequest, Result<GetByIdProductResponce>>
    {
        private readonly IRepository<Product, int> _repository;
        private readonly IInputValidator<GetByIdProductRequest> _validator;
        private readonly ILogger<GetByIdProductRequest> _logger;

        public GetByIdProductRequestHandler(
            IRepository<Product, int> repository,
            IInputValidator<GetByIdProductRequest> validator,
            ILogger<GetByIdProductRequest> logger
            )
        {
            _repository = repository;
            _validator = validator;
            _logger = logger;
        }
        public async Task<Result<GetByIdProductResponce>> Handle(GetByIdProductRequest request, CancellationToken cancellationToken)
        {
            Result result = _validator.ValidateInput(request);

            if(result.IsFailure)
            {
                _logger.LogWarning("Validation failed for request {@request}", request);
                return result;
            }

            Product? product = await _repository.GetById(request.ProductId);

            if(product == null)
            {
                _logger.LogWarning("No product was found with the id: {request.ProductId}", request.ProductId);
                return Result<GetByIdProductResponce>.Failure($"No Product was found with the id:{request.ProductId}");
            }

            return Result<GetByIdProductResponce>.Success("Product getted successfuly", product);
        }
    }
}
