using MySqlConnector;
using TrainingLog;
using TrainingLog.Storage;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

WatchDB WatchDB = new WatchDB();
DataStorage DataStorages = new DataStorage();

app.MapPost("/isLoginValid", WatchDB.CheckLogin);

app.MapPost("/LoginUser", DataStorages.Login);

app.MapPost("/GetList", WatchDB.List);

app.Run();

