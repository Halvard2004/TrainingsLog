namespace TrainingLog.Storage;

public static class Objects
{
    public record LoginInput(string Username, string Password);
    public record LoginData(int Id, string Username);

    public record Task(Guid Id, int UserConnection, string LogText, DateTime Date, TimeSpan StartTime, TimeSpan EndTime);
    
    public record GetTask(int Id);
    
    public record Tag(Guid Id, string Title, int User_Id);
    
    public record TagLogConnection(Guid Id, Guid Log_Id, Guid Tag_Id);
}