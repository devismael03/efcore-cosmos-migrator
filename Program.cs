using EfCoreCosmosMigrationEngine.Data;
using EfCoreCosmosMigrationEngine.MigrationEngine;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<LibraryDbContext>(options =>
{
    options.UseCosmos(
        builder.Configuration["CosmosDb:AccountEndpoint"],
        builder.Configuration["CosmosDb:AccountKey"],
        builder.Configuration["CosmosDb:DatabaseName"]
    );

});

builder.Services.AddSingleton<IMigrationEngine, MigrationEngine>();


var app = builder.Build();

Task.Run(() =>
{
    using (var scope = app.Services.CreateScope())
    {
        var dbContext = scope.ServiceProvider.GetRequiredService<LibraryDbContext>();
        dbContext.Database.EnsureCreated();

        var engine = scope.ServiceProvider.GetRequiredService<IMigrationEngine>();
        engine.HandleMigration(scope.ServiceProvider.GetRequiredService<LibraryDbContext>);
    }
});


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
