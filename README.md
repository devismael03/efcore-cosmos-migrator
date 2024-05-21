# About
EfCoreCosmosMigrator is an open-source project to address current incapability in Entity Framework Core Cosmos DB integration. At this moment, EFCore CosmosDB integration does not support creating migrations (as cosmos db is schemaless).
However, we had a need to seed data into cosmos, change the default data at some points of development and production phases (in a persisted manner as code, not scripts). Therefore, we have created a simple migration engine which takes migrations from assembly
of current project, applies them in sorted order and appends them to migration log table in cosmos to prevent them being applied again in next run.

# How to use

1. First, configure your Cosmos DB connection with your own credentials.
2. Add MigrationEngine to service IoC.

3. Add following to start of app lifecycle in Program.cs  to let migrations be applied on run. (change LibraryDbContext to your cosmos db context)

```
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
```

4. Create a MigrationLogEntity class in your project (sample given in repository) and add it to your context as new cosmosdb collection.
5. Now you can start creating migrations. For this, you just have to create classes that extends IMigration interface and has naming which can help migration engine in ordering. (for example datetime can be a good approach (if in same day add 01,02 to end))
   

