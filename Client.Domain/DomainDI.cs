using Client.Domain.Dto;
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
            services.AddScoped<PaymentValidationPolicy>();
            services.AddScoped<PaymentInDebtValidationPolicy>();

            return services;
        }
    }
}
