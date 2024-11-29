using DBService.Helpers;
using Microsoft.Extensions.DependencyInjection;

namespace DBService.Extensions
{
    public static class AutoMapperConfiguration
    {
        public static IServiceCollection AddAutoMapperProfiles(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(AutoMapperProfile));
            return services;
        }
    }
}
