using Client.API.Endpoints;
using Client.API.Middlewares;
using Client.Application;
using Client.Domain;
using Client.Infrastructure;
using Client.Infrastructure.Configuration;

namespace Client.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.Configure<MongoDbSettings>(
                builder.Configuration.GetSection("MongoDbSettings"));

            builder.Services.AddInfrastructure(builder.Configuration);
            builder.Services.AddApplication();
            builder.Services.AddDomain();

            builder.Services.AddAuthorization();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseHttpsRedirection();

            app.UseMiddleware<ValidationExceptionMiddleware>();

            app.UseAuthorization();

            using (var scope = app.Services.CreateScope())
            {
                var initializer = scope.ServiceProvider.GetRequiredService<MongoInitializer>();
                await initializer.EnsureIndexesCreatedAsync();
            }

            app.MapClientEndpoints();

            app.Run();
        }
    }
}
