using CSharp.Data.Interfaces.Production;
using CSharp.Data.Repositories.Production;

namespace CSharp.API.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureDataLayerServices(this IServiceCollection services)
        {
            services.AddScoped<IProductRepository, ProductRepository>();
        }
    }
}
