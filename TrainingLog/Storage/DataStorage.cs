using System.Data;
using MySqlConnector;

namespace TrainingLog.Storage;

public class DataStorage
{
    public static MySqlConnection ConnectToDatabase()
    {
        string constring = "server=localhost;user id=TestUser;password=Password12345;database=traininglog;";
        MySqlConnection connection = new MySqlConnection(constring);
        connection.Open();
        return connection;
    }

    public static void EndConnection(MySqlConnection connection)
    {
        connection.Close();
    }

    public static bool IsUserAccountValid(MySqlConnection connection, LoginInput text)
    {
        var query = $"select * from users where username = '{text.Username}' and  password = '{text.Password}'";
        var response = GetValidUser(connection, query);
        return response != null ? true : false;

    }

    private static LoginData GetValidUser(MySqlConnection connection, string query)
    {
        MySqlCommand cmd = new MySqlCommand(query, connection);
        MySqlDataReader reader = cmd.ExecuteReader();
        reader.Read();
        if (reader.HasRows)
        {
            return new LoginData(reader.GetInt32("idUsers"), reader.GetString("username")); ;
        }

        return null;
    }

    public static LoginData Login(LoginInput text)
    {
        MySqlConnection connection = ConnectToDatabase();
        var query = $"select * from users where username = '{text.Username}' and  password = '{text.Password}'";
        LoginData user = GetValidUser(connection, query);
        EndConnection(connection);
        return user;
    }

    public static bool HasLogData(MySqlConnection connection, string query)
    {
        var response = GetValidLogs(connection, query);
        return response != null ? true : false;
    }

    public static List<Task> GetValidLogs(MySqlConnection connection, string query)
    {
        List<Task> task = new List<Task>();
        MySqlCommand cmd = new MySqlCommand(query, connection);
        MySqlDataReader reader = cmd.ExecuteReader();
        reader.Read();
        if (reader.HasRows)
        {
            task.Add(new Task(
            {
                Id = reader.GetInt32("idUserLog"),
                    LogText = reader.GetString("logText"),
                    Date = reader.GetDateTime("logDate")
            });

        }

        return null;
    }
}