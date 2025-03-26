

using System.Reflection;
using Catalog.Service.Features.Core;
using Catalog.Service.Features.Features.Products.Commands.Create;
using Catalog.Service.Features.Features.Products.Queries.GetAll;
using Catalog.Service.Features.Features.Products.Queries.GetById;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Catalog.Service.Features.Extensions
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
        {
            #region validators
            services.AddSingleton<IInputValidator<GetAllProductsQueryRequest>, GetAllProductsQueryValidator>();
            services.AddSingleton<IInputValidator<GetByIdProductRequest>, GetByIdProductValidator>();
            services.AddSingleton<IInputValidator<CreateProductCommand>, CreateProductValidator>();
 
            #endregion

            services.AddMediatR( m=>
            {
                m.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            });
            
            return services;
        }

    }
}
