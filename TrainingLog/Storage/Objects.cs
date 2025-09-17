namespace TrainingLog.Storage;

public static class Objects
{
    public record LoginInput(string Username, string Password);
    public record LoginData(int Id, string Username);

    public record Task(int Id, string LogText, DateTime Date);
 
    
    public record GetTask(int Id, string username);
}