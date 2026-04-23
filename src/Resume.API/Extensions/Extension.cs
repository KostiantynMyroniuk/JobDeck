using Azure.Storage.Blobs;
using Microsoft.EntityFrameworkCore;
using Resume.API.Infrastructure;
using Resume.API.Services.Azure;

namespace Resume.API.Extensions
{
    public static class Extension
    {
        public static void AddConfiguration(this IHostApplicationBuilder builder)
        {
            builder.Services.AddControllers();

            builder.Services.AddOpenApi();

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            builder.Services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);
            });

            builder.Services.AddSingleton(new BlobServiceClient(builder.Configuration["AZURE_STORAGE_CONNECTION_STRING"]));

            builder.Services.AddScoped<IFileStorage, FileStorage>();
        }

        public static async Task MigrateDatabaseAsync(this WebApplication app)
        {
            var scope = app.Services.CreateScope();

            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            await context.Database.MigrateAsync();
        }
    }
}
