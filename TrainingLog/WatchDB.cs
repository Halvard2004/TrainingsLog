using MySqlConnector;
using TrainingLog.Storage;

namespace TrainingLog;

public class WatchDb
{
    private readonly DataStorage _dataStorages = new DataStorage();
    public bool CheckLogin(Objects.LoginInput text)
    {
        MySqlConnection connection = _dataStorages.ConnectToDatabase();
        var check = _dataStorages.IsUserAccountValid(connection, text);
        _dataStorages.EndConnection(connection);
        return check;
    }

    public List<Objects.Task> List(int id)
    {
        List<Objects.Task> tasks = new List<Objects.Task>();
        MySqlConnection connection = _dataStorages.ConnectToDatabase();
        var query = $"select * from userlog where UserConnection = {id}";
        bool check = _dataStorages.HasLogData(connection, query);
        if (check)
        {
            tasks = _dataStorages.GetValidLogs(connection, query);
        }
        _dataStorages.EndConnection(connection);
        return tasks;
    }
}