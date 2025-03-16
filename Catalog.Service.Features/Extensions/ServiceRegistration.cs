

using Catalog.Service.Features.Core;
using Catalog.Service.Features.Features.Products.Queries.GetAll;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Catalog.Service.Features.Extensions
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddCrossCutting(this IServiceCollection services)
        {
            #region validators
            services.AddScoped<IInputValidator<GetAllProductsQueryRequest>, GetAllProductsQueryValidator>();

            #endregion

            return services;
        }

    }
}
