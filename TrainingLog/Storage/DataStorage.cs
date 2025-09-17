using System.Data;
using MySqlConnector;

namespace TrainingLog.Storage;

public class DataStorage
{
    public MySqlConnection ConnectToDatabase()
    {
        string constring = "server=localhost;user id=TestUser;password=Password12345;database=traininglog;";
        MySqlConnection connection = new MySqlConnection(constring);
        connection.Open();
        return connection;
    }

    public void EndConnection(MySqlConnection connection)
    {
        connection.Close();
    }

    public bool IsUserAccountValid(MySqlConnection connection, Objects.LoginInput text)
    {
        var query = $"select * from users where username = '{text.Username}' and  password = '{text.Password}'";
        var response = GetValidUser(connection, query);
        return response != null ? true : false;

    }

    private Objects.LoginData GetValidUser(MySqlConnection connection, string query)
    {
        MySqlCommand cmd = new MySqlCommand(query, connection);
        MySqlDataReader reader = cmd.ExecuteReader();
        reader.Read();
        if (reader.HasRows)
        {
            return new Objects.LoginData(reader.GetInt32("idUsers"), reader.GetString("username")); ;
        }

        return null;
    }

    public Objects.LoginData Login(Objects.LoginInput text)
    {
        MySqlConnection connection = ConnectToDatabase();
        var query = $"select * from users where username = '{text.Username}' and  password = '{text.Password}'";
        Objects.LoginData user = GetValidUser(connection, query);
        EndConnection(connection);
        return user;
    }

    public bool HasLogData(MySqlConnection connection, string query)
    {
        var response = GetValidLogs(connection, query);
        return response != null ? true : false;
    }

    public List<Objects.Task> GetValidLogs(MySqlConnection connection, string query)
    {
        List<Objects.Task> tasks = new List<Objects.Task>();
        MySqlCommand cmd = new MySqlCommand(query, connection);
        MySqlDataReader reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            var id = reader.GetInt32("idUserLog");
            var userLog = reader.GetString("LogText");
            var date = reader.GetDateTime("Date");
            tasks.Add(new Objects.Task (id, userLog, date));
        }
        reader.Close();
        return tasks;
    }
}