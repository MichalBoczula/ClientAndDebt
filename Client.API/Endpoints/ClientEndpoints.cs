using Client.Application.Services;
using Client.Domain.Dto;
using Client.Domain.Models;
using Client.Domain.Validation.Common;
using Client.Domain.Validation.Concrete.Policies;

namespace Client.API.Endpoints
{
    // TODO: Test containers for acceptance tests and examples
    // TODO: refactor endpoints with validationrules
    // TODO: endpoit should return 201 (post) or 204 (put)
    public static class ClientEndpoints
    {
        public static void MapClientEndpoints(this WebApplication app)
        {
            app.MapPost("/clients", async (ClientInstance client, ClientService service) =>
            {
                await service.AddClientAsync(client);
                return Results.Created($"/clients/{client.Id}", client);
            });

            app.MapPut("/clients/{clientId:guid}/debts", async (Guid clientId, DebtDto debt, ClientService service) =>
            {
                var success = await service.AddDebtToClientAsync(clientId, debt);
                return success ? Results.Ok() : Results.NotFound();
            });

            app.MapPut("/clients/{clientId:guid}/debts/{debtId:guid}/payments", 
                async (Guid clientId, Guid debtId, Payment payment, ClientService service) =>
            {
                var success = await service.AddPaymentToDebtAsync(clientId, debtId, payment);
                return success ? Results.Ok() : Results.NotFound();
            });

            app.MapGet("/clients/{id:guid}", async (Guid id, ClientService service) =>
            {
                var result = await service.GetClientByIdAsync(id);
                return result is not null ? Results.Ok(result) : Results.NotFound();
            });

            app.MapGet("/validationRules", () =>
            {
                var policies = new List<ValidationPolicyDescriptor>();
                var clientValidationPolicy = new ClientValidationPolicy();
                var debtValidationPolicy = new DebtValidationPolicy();
                var paymentValidationPolicy = new PaymentValidationPolicy();
                var paymentInDebtValidationPolicy = new PaymentInDebtValidationPolicy();

                policies.Add(clientValidationPolicy.Describe());
                policies.Add(debtValidationPolicy.Describe());
                policies.Add(paymentValidationPolicy.Describe());
                policies.Add(paymentInDebtValidationPolicy.Describe());

                return policies.Any() ? Results.Ok(policies) : Results.NotFound();
            });
        }
    }
}
