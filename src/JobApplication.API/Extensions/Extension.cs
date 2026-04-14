using JobApplication.API.Features;
using JobApplication.API.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace JobApplication.API.Extensions
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

            builder.Services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<ApplicationProfile>();
            });
        }
    }
}
