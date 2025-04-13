using Inventory.Service.Infraestructure.Extensions;
using Microsoft.AspNetCore.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddInfraestructureLayer(builder.Configuration);
builder.Services.AddHealthChecks();
builder.Services.AddRateLimiter(option =>
{
    //TODO: finish this later, investigate too
    option.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
    option.AddFixedWindowLimiter("fixed", Options =>
    {
        Options.Window = TimeSpan.FromMicroseconds(2);
        Options.PermitLimit = 4; // Amount of allowed request
    });
    option.OnRejected = async (context, toke) =>
    {
        await context.HttpContext.Response.WriteAsync("Rejected");
    };
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseHealthChecks("/health");
app.UseAuthorization();
app.UseRateLimiter();

app.MapControllers();

app.Run();
