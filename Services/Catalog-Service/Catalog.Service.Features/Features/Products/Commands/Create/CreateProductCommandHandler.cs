using Catalog.Service.Domain.Entities;
using Catalog.Service.Domain.Repository;
using Catalog.Service.Features.Core;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Catalog.Service.Features.Features.Products.Commands.Create;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Result<CreateProductResponse>>
{
    private readonly IRepository<Product, int> _productRepository;
    private readonly IInputValidator<CreateProductCommand> _validator;
    private readonly ILogger<CreateProductCommand> _logger;

    public CreateProductCommandHandler(IRepository<Product,int>productRepository, IInputValidator<CreateProductCommand> validator, ILogger<CreateProductCommand> logger)
    {
        _productRepository = productRepository;
        _validator = validator;
        _logger = logger;
    }
    public async Task<Result<CreateProductResponse>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        Result validationResult = _validator.ValidateInput(request);

        if (validationResult.IsFailure)
        {
            _logger.LogWarning("Validation failed with the following errors: {Message}", validationResult.Message);
            return validationResult;
        }
        
        Product? productCreated = await _productRepository.CreateAsync( new()
        {
            Name = request.Name,
            Description = request.Description,
            Price = request.Price,
        }, cancellationToken);

        if (productCreated == null)
        {
            _logger.LogWarning("Product could not be created, with the following request: {Product}", request);
            return Result<CreateProductResponse>.Failure("Product could not be created.");
        }
        
        return Result<CreateProductResponse>.Success("Product successfully created.", productCreated);
    }
}