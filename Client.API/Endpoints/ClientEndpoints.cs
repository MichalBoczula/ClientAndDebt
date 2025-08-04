using Client.Application.Services;
using Client.Domain.Models;
using Client.Domain.Validation.Concrete.Policies;

namespace Client.API.Endpoints
{
    public static class ClientEndpoints
    {
        public static void MapClientEndpoints(this WebApplication app)
        {
            app.MapPost("/clients", async (ClientInstance client, ClientService service) =>
            {
                await service.AddClientAsync(client);
                return Results.Created($"/clients/{client.Id}", client);
            });

            app.MapPut("/clients/{clientId:guid}/debts", async (Guid clientId,
                                                                Debt debt,
                                                                ClientService service) =>
            {
                var success = await service.AddDebtToClientAsync(clientId, debt);
                return success ? Results.Ok() : Results.NotFound();
            });

            app.MapGet("/clients/{id:guid}", async (Guid id, ClientService service) =>
            {
                var result = await service.GetClientByIdAsync(id);
                return result is not null ? Results.Ok(result) : Results.NotFound();
            });

            app.MapGet("/validationRules", () =>
            {
                var clientValidationPolicy = new ClientValidationPolicy();
                var debtValidationPolicy = new DebtValidationPolicy();
                var clientResult = clientValidationPolicy.Describe();
                var debtResult = debtValidationPolicy.Describe();

                return clientResult is not null ? Results.Ok(clientResult) : Results.NotFound();
            });
        }
    }
}
