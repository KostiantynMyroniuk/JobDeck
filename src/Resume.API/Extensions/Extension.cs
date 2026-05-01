using Azure.Storage.Blobs;
using MediatR;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Azure;
using Resume.API.Behaviors;
using Resume.API.Infrastructure;
using Resume.API.Services.Azure;
using System.ComponentModel.DataAnnotations;

namespace Resume.API.Extensions
{
    public static class Extension
    {
        public static void AddConfiguration(this IHostApplicationBuilder builder)
        {
            builder.Services.AddControllers();

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            builder.Services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);
                cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            });

            builder.Services.AddAzureClients(clientBuilder =>
            {
                clientBuilder.AddBlobServiceClient(builder.Configuration["AZURE_STORAGE_CONNECTION_STRING"]);
            });

            builder.Services.AddScoped<IFileStorage, FileStorage>();
        }

        public static async Task MigrateDatabaseAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();

            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            await context.Database.MigrateAsync();
        }
    }
}
