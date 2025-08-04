using Client.Domain.Validation.Concrete.Policies;
using Microsoft.Extensions.DependencyInjection;

namespace Client.Domain
{
    public static class DomainDI
    {
        public static IServiceCollection AddDomain(this IServiceCollection services)
        {
            services.AddScoped<ClientValidationPolicy>();
            services.AddScoped<DebtValidationPolicy>();

            return services;
        }
    }
}
