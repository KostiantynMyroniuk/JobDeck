using JobApplication.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.AddConfiguration();

builder.AddMigrations();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
