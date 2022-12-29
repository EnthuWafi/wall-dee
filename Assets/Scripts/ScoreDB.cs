using Mono.Data.Sqlite;
using System.Collections.Generic;
using UnityEngine;

public class ScoreDB : MonoBehaviour
{
    // Start is called before the first frame update

    private string dbName;
    void Start()
    {
        dbName = "URI=file:Score.db";
        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "CREATE TABLE IF NOT EXISTS scores (id INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL, name TEXT, score NUMERIC);";
                command.ExecuteNonQuery();
            }

            connection.Close();
        }
    }

    public void AddScore(string name, float score)
    {
        //sql
        if (!checkIfSafe(name))
        {
            Debug.Log("Unsafe SQL !");
            return;      
        }
        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = string.Format("INSERT INTO scores(name, score) VALUES ('{0}', {1});", name, score);
                command.ExecuteNonQuery();
            }

            connection.Close();
        }
        Debug.Log("Score added.");
    }

    public void Clear()
    {
        //sql
        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "DELETE FROM scores";
                command.ExecuteNonQuery();
            }

            connection.Close();
        }
        Debug.Log("Scores cleared.");
    }
    public List<Dictionary<string, string>> GetListOfScores()
    {
        //sql
        List<Dictionary<string, string>> listOfScores = new List<Dictionary<string, string>>();

        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT name, score FROM scores ORDER BY score DESC;";

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Dictionary<string, string> dict = new Dictionary<string, string>();
                        dict.Add("name", reader["name"].ToString());
                        dict.Add("score", reader["score"].ToString());

                        listOfScores.Add(dict);
                    }
                    reader.Close();
                }
            }

            connection.Close();
        }
        return listOfScores;
    }

    public bool checkIfSafe(params string[] str)
    {
        foreach (string s in str)
        {
            if (s.Contains("\'") || s.Contains("\""))
            {
                return false;
            }
        }
        return true;
    }
}
