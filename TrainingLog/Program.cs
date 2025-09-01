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



app.MapPost("/isLoginValid", WatchDB.CheckLogin);

app.MapPost("/LoginUser", DataStorage.Login);

app.MapPost("/GetList", WatchDB.List);

app.Run();

public record LoginInput(string Username, string Password);
public record LoginData(int Id, string Username);
public record Task(int Id, string LogText, DateTime Date);
