using MySqlConnector;
using TrainingLog.Storage;

namespace TrainingLog;

public class WatchDB
{
    public static bool CheckLogin(LoginInput text)
    {
        MySqlConnection connection = DataStorage.ConnectToDatabase();
        var check = DataStorage.IsUserAccountValid(connection, text);
        DataStorage.EndConnection(connection);
        return check;
    }

    public static List<Task> List(int id)
    {
        List<Task> tasks = new List<Task>();
        MySqlConnection connection = DataStorage.ConnectToDatabase();
        var query = $"select * from userlog where UserConnection = 'id'";
        bool check = DataStorage.HasLogData(connection, query);
        if (check)
        {
            tasks = DataStorage.GetValidLogs(connection, query);
        }
        DataStorage.EndConnection(connection);
        return tasks;
    }
}