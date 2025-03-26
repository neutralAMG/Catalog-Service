using Catalog.Service.Api.Endpoints;
using Catalog.Service.Api.Extensions;
using Catalog.Service.Infraestructure.Extensions;
using Catalog.Service.CrossCuttingConcerns.Extensions;
using Catalog.Service.Features.Extensions;
using Microsoft.AspNetCore.RateLimiting;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddInfrastructurLayer(builder.Configuration);
builder.Services.AddCrossCutting(builder.Host);
builder.Services.AddApplicationLayer();
builder.Services.AddHealthChecks();
builder.Services.AddRateLimiter(op =>
{
    op.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
    op.AddFixedWindowLimiter("fixed", options =>
    {
        options.Window = TimeSpan.FromSeconds(5);
        options.PermitLimit = 5;
    } );
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseExceptionHandler("/erros");
app.UseRateLimiter();
app.MapHealthChecks("/health");

app.AddCheckAndAutoMigrate();
app.AddProductEndpoints();

app.Run();

