using Azure.Core;
using Catalog.Service.Features.Core;
using Catalog.Service.Features.Features.Products.Commands.Create;
using Catalog.Service.Features.Features.Products.Queries.GetAll;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Service.Api.Endpoints;

public static class ProductEndpoints
{
    public static WebApplication AddProductEndpoints(this WebApplication app)
    {
        app.MapGet("/products", async ([FromServices] ISender sender, CancellationToken cancellationToken) =>
        { 
            GetAllProductsQueryRequest request = new GetAllProductsQueryRequest();
            Result<GetAllProductsQueryResponce> response = await sender.Send(request:  request, cancellationToken: cancellationToken);

             return response.IsFailure ? Results.BadRequest(response) : Results.Ok(response);
    
        }).WithName("GetProducts")
        .WithSummary("Get all products")
        .Produces<GetAllProductsQueryResponce>()
        .RequireRateLimiting("fixed");
        
        app.MapPost("/products/add", async ([FromServices] ISender sender, CreateProductCommand request, CancellationToken cancellationToken) =>
            { 

                Result<CreateProductResponse> response = await sender.Send(request:  request, cancellationToken: cancellationToken);

                return response.IsFailure ? Results.BadRequest(response) : Results.Ok(response);
    
            }).WithName("AddProduct")
            .WithSummary("Adds a new product")
            .Produces<GetAllProductsQueryResponce>()
            .RequireRateLimiting("fixed");
        
        return app;
    }
    
    
}