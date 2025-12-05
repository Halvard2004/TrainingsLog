using System.Data;
using MySqlConnector;

namespace TrainingLog.Storage;

public class DataStorage
{
    public MySqlConnection ConnectToDatabase()
    {
        string constring = "server=localhost;user id=TrainingLog;password=TrainingLog1;database=traininglog;";
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
        var query = $"select * from users where Username = '{text.Username}' and  Password = '{text.Password}'";
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
            return new Objects.LoginData(reader.GetInt32("Id"), reader.GetString("Username")); ;
        }

        return null;
    }

    public Objects.LoginData Login(Objects.LoginInput text)
    {
        MySqlConnection connection = ConnectToDatabase();
        var query = $"select * from users where Username = '{text.Username}' and  Password = '{text.Password}'";
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
            var id = reader.GetGuid("Id");
            var userId = reader.GetInt32("UserConnection");
            var userLog = reader.GetString("LogText");
            var date = reader.GetDateTime("Date");
            var startTime = reader.GetTimeSpan("StartTime");
            var endTime = reader.GetTimeSpan("EndTime");
            tasks.Add(new Objects.Task (id, userId, userLog, date, startTime, endTime ));
        }
        reader.Close();
        return tasks;
    }

    public void AddLog(Objects.Task task)
    {
        Console.Write(task);
        MySqlConnection connection = ConnectToDatabase();
        MySqlCommand cmd = new MySqlCommand("INSERT INTO userlog (`Id`, `UserConnection`, `LogText`, `Date`, `StartTime`, `EndTime`)VALUES (@Id, @UserConnection, @LogText, @Date, @StartTime, @EndTime)", connection);
        cmd.Parameters.AddWithValue("@Id", task.Id);
        cmd.Parameters.AddWithValue("@UserConnection", task.UserConnection);
        cmd.Parameters.AddWithValue("@LogText", task.LogText);
        cmd.Parameters.AddWithValue("@Date", task.Date);
        cmd.Parameters.AddWithValue("@StartTime", task.StartTime);
        cmd.Parameters.AddWithValue("@EndTime", task.EndTime);
        cmd.ExecuteNonQuery();
        EndConnection(connection);
    }

    public void AddTag(Objects.Tag tag)
    {
        MySqlConnection connection = ConnectToDatabase();
        MySqlCommand cmd = new MySqlCommand("INSERT INTO tags (`Id`, `Title`, `User_Id`) VALUES (@Id, @Title, @User_Id)", connection);
        cmd.Parameters.AddWithValue("@Id", tag.Id);
        cmd.Parameters.AddWithValue("@Title", tag.Title);
        cmd.Parameters.AddWithValue("@User_Id", tag.User_Id);
        cmd.ExecuteNonQuery();
        EndConnection(connection);
        
    }

    public void AddLogTagConnection(Objects.TagLogConnection tagLogConnection)
    {
        MySqlConnection connection = ConnectToDatabase();
        MySqlCommand cmd = new MySqlCommand("INSERT INTO log_tags (`Id`, `Log_Id`, `Tag_Id`) VALUES (@Id, @Log_Id, @Tag_Id)", connection);
        cmd.Parameters.AddWithValue("@Id", tagLogConnection.Id);
        cmd.Parameters.AddWithValue("@Log_Id", tagLogConnection.Log_Id);
        cmd.Parameters.AddWithValue("@Tag_Id", tagLogConnection.Tag_Id);
        cmd.ExecuteNonQuery();
        EndConnection(connection);
    }

    public List<Objects.Tag> GetValidTags(int UserId)
    {
        List<Objects.Tag> tags = new List<Objects.Tag>();
        MySqlConnection connection = ConnectToDatabase();
        var query = $"select * from tags where User_Id = {UserId}";
        MySqlCommand cmd = new MySqlCommand(query, connection);
        MySqlDataReader reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            var id = reader.GetGuid("Id");
            var title = reader.GetString("Title");
            var userId = reader.GetInt32("User_Id");
            tags.Add(new Objects.Tag(id, title, userId));
        }
        reader.Close();
        return tags;
    }


    public List<Guid> GetValidLogIdsWithTagId(Guid tagId)
    {
        List<Guid> tasks = new List<Guid>();
        MySqlConnection connection = ConnectToDatabase();
        var query = $"select * from log_tags where Tag_Id = '{tagId}'";
        MySqlCommand cmd = new MySqlCommand(query, connection);
        MySqlDataReader reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            var logId = reader.GetGuid("Log_Id");
            tasks.Add(logId);
        }
        reader.Close();
        return tasks;
    }


    public void EditLog(Guid id, Objects.Tag tag)
    {
        MySqlConnection connection = ConnectToDatabase();
        var query = $"UPDATE `tags` SET `Title` = @Title WHERE (`Id` = @Id)";
        
        MySqlCommand cmd = new MySqlCommand(query, connection);
        cmd.Parameters.AddWithValue("@Id", id); 
        cmd.Parameters.AddWithValue("@Title", tag.Title);
        cmd.ExecuteNonQuery();
        EndConnection(connection);
    }

    public void Deletetag(Guid id)
    {
        MySqlConnection connection = ConnectToDatabase();
        var query = $"DELETE FROM tags WHERE `Id` = @Id";
        MySqlCommand cmd = new MySqlCommand(query, connection);
        cmd.Parameters.AddWithValue("@Id", id);
        cmd.ExecuteNonQuery();
        
        query = $"DELETE FROM log_tags WHERE `Tag_Id` = @Id";
        MySqlCommand cmd2 = new MySqlCommand(query, connection);
        cmd2.Parameters.AddWithValue("@Id", id);
        cmd2.ExecuteNonQuery();
        
        EndConnection(connection);
    }

    public Objects.Task GetValidLog(Guid id)
    {
        Objects.Task task;
        MySqlConnection connection = ConnectToDatabase();
        var query = $"SELECT * FROM userlog WHERE `Id` = @Id";
        MySqlCommand cmd = new MySqlCommand(query, connection);
        cmd.Parameters.AddWithValue("@Id", id);
        MySqlDataReader reader = cmd.ExecuteReader();
        reader.Read();
        if (reader.HasRows)
        { 
            task = new Objects.Task(reader.GetGuid("Id"), reader.GetInt32("UserConnection"), reader.GetString("LogText"), reader.GetDateTime("Date"), reader.GetTimeSpan("StartTime"), reader.GetTimeSpan("EndTime")); ;
        }
        else
        {
            task = null;
        }
        reader.Close();
        EndConnection(connection);
        return task;
        
    }

    public Objects.Tag GetTagWithLogId(Guid id)
    {
        Guid tagId;
        Objects.Tag tag;
        MySqlConnection connection = ConnectToDatabase();
        var query = $"select * from log_tags where Log_Id = @Id";
        MySqlCommand cmd = new MySqlCommand(query, connection);
        cmd.Parameters.AddWithValue("@Id", id);
        MySqlDataReader reader = cmd.ExecuteReader();
        reader.Read();
        if (reader.HasRows)
        {
            tagId = reader.GetGuid("Tag_Id");
        }
        else
        {
            return null;
        }
        reader.Close();
        query = $"select * from tags where Id = @tagId";
        cmd = new MySqlCommand(query, connection);
        cmd.Parameters.AddWithValue("@tagId", tagId);
        reader = cmd.ExecuteReader();
        reader.Read();
        {
            if (reader.HasRows)
            { 
                tag = new Objects.Tag(reader.GetGuid("Id"), reader.GetString("Title"), reader.GetInt32("User_Id"));
            }
            else
            {
                tag = null;
            }
        }
        reader.Close();
        EndConnection(connection);
        return tag;
    }
}