using Client.Domain.Repositories;
using Client.Infrastructure.Configuration;
using Client.Infrastructure.Context;
using Client.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Client.Infrastructure
{
    public static class InfrastructureDI
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ClientDbContext>();
            services.AddScoped<MongoInitializer>();
            services.AddScoped<IClientRepository, ClientRepository>();

            return services;
        }
    }
}
