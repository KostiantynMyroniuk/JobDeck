using JobApplication.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.AddConfiguration();

var app = builder.Build();

await app.MigrateDatabaseAsync();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
