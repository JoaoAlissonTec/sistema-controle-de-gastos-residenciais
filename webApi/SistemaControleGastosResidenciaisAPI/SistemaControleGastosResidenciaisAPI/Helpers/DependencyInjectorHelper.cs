using SistemaControleGastosResidenciaisAPI.Repositories;
using SistemaControleGastosResidenciaisAPI.Services;

namespace SistemaControleGastosResidenciaisAPI.Helpers
{
    public static class DependencyInjectorHelper
    {
        public static void AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IPersonRepository, PersonRepository>();
            services.AddScoped<IPersonService, PersonService>();

            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ICategoryService, CategoryService>();
        }
    }
}
