using MySqlConnector;
using TrainingLog.Storage;

namespace TrainingLog;

public class WatchDB
{
    private readonly DataStorage _dataStorages = new DataStorage();
    public bool CheckLogin(Objects.LoginInput text)
    {
        MySqlConnection connection = _dataStorages.ConnectToDatabase();
        var check = _dataStorages.IsUserAccountValid(connection, text);
        _dataStorages.EndConnection(connection);
        return check;
    }

    public List<Objects.Task> List(Objects.GetTask user)
    {
        List<Objects.Task> tasks = new List<Objects.Task>();
        MySqlConnection connection = _dataStorages.ConnectToDatabase();
        var query = $"select * from userlog where UserConnection = {user.Id}";
        bool check = _dataStorages.HasLogData(connection, query);
        if (check)
        {
            tasks = _dataStorages.GetValidLogs(connection, query);
        }
        _dataStorages.EndConnection(connection);
        return tasks;
    }
}