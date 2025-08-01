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

            app.MapGet("/clients/{id:guid}", async (Guid id, ClientService service) =>
            {
                var result = await service.GetClientByIdAsync(id);
                return result is not null ? Results.Ok(result) : Results.NotFound();
            });

            app.MapGet("/validationRules", () =>
            {
                var clientValidationPolicy = new ClientValidationPolicy();
                var result = clientValidationPolicy.Describe();

                return result is not null ? Results.Ok(result) : Results.NotFound();
            });
        }
    }
}
