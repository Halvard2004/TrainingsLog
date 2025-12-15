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


WatchDb watchDb = new WatchDb();
DataStorage dataStorages = new DataStorage();

app.MapPost("/isLoginValid", watchDb.CheckLogin);

app.MapPost("/LoginUser", dataStorages.Login);

app.MapGet("/GetList/{id:int}", (int id) =>
{
    var list = watchDb.List(id);
    return Task.FromResult(list);
});

app.MapPost("/CreateLog", dataStorages.AddLog);

app.MapPost("/CreateTag", dataStorages.AddTag);

app.MapPost("/CreateLogTagConnection", dataStorages.AddLogTagConnection);

app.MapGet("/GetTags/{id:int}", (int id) => Task.FromResult(dataStorages.GetValidTags(id)));

app.MapGet("/GetTagFromLogId/{id:Guid}", (Guid id) => dataStorages.GetTagWithLogId(id));

app.MapGet("/GetLog/{id}", (Guid id) => Task.FromResult(dataStorages.GetValidLog(id)));

app.MapGet("/GetLogListWithTag/{id:Guid}", (Guid id) => dataStorages.GetValidLogIdsWithTagId(id));

app.MapPut("/EditTag/{id}", (Guid id, Objects.Tag tag) => dataStorages.EditTag(id, tag));

app.MapDelete("/DeleteTag/{id}", (Guid id) => dataStorages.Deletetag(id));

app.MapDelete("/DeleteLog/{id}", (Guid id) => dataStorages.DeleteLog(id));

app.MapPut("/UpdateLog/{id}", (Guid id, Objects.Task task) => dataStorages.EditLog(id, task));

app.MapPut("/UpdateLogTag/{logId}/{tagId}", (Guid logId, Guid tagId) => dataStorages.EditLogTag(logId, tagId));

app.Run();

