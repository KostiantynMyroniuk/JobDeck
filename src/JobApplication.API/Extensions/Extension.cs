using FluentValidation;
using JobApplication.API.Behaviors;
using JobApplication.API.Features;
using JobApplication.API.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace JobApplication.API.Extensions
{
    public static class Extension
    {
        public static void AddConfiguration(this IHostApplicationBuilder builder)
        {
            builder.Services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Converters
                    .Add(new JsonStringEnumConverter());
                });

            builder.Services.AddOpenApi();

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            builder.Services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);
                cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            });

            builder.Services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<ApplicationProfile>();
            });

            builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);
        }

        public static async Task MigrateDatabaseAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();

            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            await context.Database.MigrateAsync();
        }
    }
}
