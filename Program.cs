using Store.API.Data;
using Store.API.Endpoints;

var builder = WebApplication.CreateBuilder(args);

var connString = builder.Configuration.GetConnectionString("Store");

builder.Services.AddSqlite<StoreContext>(connString); //behind the scene it uses AddScoped<StoreContext>()

var app = builder.Build();

app.MapGamesEndpoints();

app.MigrateDb();

app.Run();
