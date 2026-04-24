using Resume.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.AddConfiguration();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    await app.MigrateDatabaseAsync();
}

app.MapControllers();

app.Run();
