﻿using System.Data;
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

    public List<Objects.Tag> GetValidTags(int UserId)
    {
        List<Objects.Tag> tags = new List<Objects.Tag>();
        MySqlConnection connection = ConnectToDatabase();
        var query = $"select * from tags where User_Id = {UserId}";
        MySqlCommand cmd = new MySqlCommand(query, connection);
        MySqlDataReader reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            var id = reader.GetInt32("Id");
            var title = reader.GetString("Title");
            var userId = reader.GetInt32("User_Id");
            tags.Add(new Objects.Tag(id, title, userId));
        }
        reader.Close();
        return tags;
    }


    public GetValidLogsWithTag(int TagId)
    {
        List<Objects.Task> tasks = new List<Objects.Task>();
        MySqlConnection connection = ConnectToDatabase();
        // Continue with everythign below here
        var query = $"select * from userlog where UserConnection = {TagId}";
        GetValidLogs(connection, query);
    }
}