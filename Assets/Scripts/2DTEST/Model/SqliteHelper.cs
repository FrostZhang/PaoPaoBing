using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using System.IO;
#endif
using Mono.Data.Sqlite;
using UnityEngine;

public class SqliteHelper : MonoBehaviour
{
    private SqliteConnection connection;
    private SqliteCommand command;
    private SqliteDataReader reader;
    public string sqlPath;


    private void Awake()
    {
        sqlPath = "data source=" + Application.streamingAssetsPath + "/" + this.sqlPath;
    }

    // Use this for initialization
    void Start()
    {

    }

#if UNITY_EDITOR
    public void CreateSqlTabel()
    {
        if (!File.Exists(sqlPath))
        {
            this.connection = new SqliteConnection(sqlPath);
        }
    }
#endif

    //打开数据库 
    public void OpenSQLaAndConnect()
    {
        this.connection = new SqliteConnection(sqlPath);
        this.connection.Open();
    }

    public SqliteDataReader ExecueteSQLCommand(string com)
    {
        command = new SqliteCommand(com);
        return command.ExecuteReader(System.Data.CommandBehavior.Default);
    }

    public void CloseSQLConnection()
    {
        if (command != null)
        {
            command.Cancel();
        }
        if (reader != null)
        {
            reader.Close();
        }
        if (connection != null)
        {
            connection.Close();
        }
        command = null;
        reader = null;
        connection = null;
        Debug.Log("已经关闭数据库");
        //Application.logMessageReceived
    }

}
