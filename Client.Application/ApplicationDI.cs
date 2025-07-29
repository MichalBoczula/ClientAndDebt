using Client.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Client.Application
{
    public static class ApplicationDI
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<ClientService>();

            return services;
        }
    }
}
