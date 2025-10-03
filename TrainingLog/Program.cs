using MySqlConnector;
using TrainingLog;
using TrainingLog.Storage;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(o => o.AddPolicy("frontend",
    p => p.WithOrigins("*")
        .AllowAnyHeader()
        .AllowAnyMethod()
));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("frontend");
app.UseHttpsRedirection();


WatchDb WatchDB = new WatchDb();
DataStorage DataStorages = new DataStorage();

app.MapPost("/isLoginValid", WatchDB.CheckLogin);

app.MapPost("/LoginUser", DataStorages.Login);

app.MapGet("/GetList/{id}", async (int id) =>
{
    var list = WatchDB.List(id);
    return list;
});

app.MapPost("/CreateLog", DataStorages.AddLog);

app.Run();

