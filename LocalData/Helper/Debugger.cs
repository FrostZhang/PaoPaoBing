using System;
using System.Collections.Generic;
using UnityEngine;

public class Debugger
{
    public static Dictionary<string, Debugger> Loggers = new Dictionary<string, Debugger>();

    public static Debugger App = Debugger.Create("App", "#FFD39B");

    public static Debugger Game= Debugger.Create("Game", "#836FFF");//8470FF

    public static Debugger Resource = Debugger.Create("Resource", "#EE7600");//#FF6EB4

    public static Debugger Network = Debugger.Create("NET", "#1E90FF");

    public static Debugger UI = Debugger.Create("UI", "#54FF9F"); 	//#7CCD7C

    public bool OpenDebug = true;

    private string module;

    private string color;

    public string Module
    {
        get
        {
            return this.module;
        }
    }

    private Debugger(string module, string color)
    {
        this.module = module;
        this.color = color;
    }

    public static Debugger Create(string module, string color = "black")
    {
        bool flag = Debugger.Loggers.ContainsKey(module);
        Debugger result;
        if (flag)
        {
            result = Debugger.Loggers[module];
        }
        else
        {
            Debugger debugger = new Debugger(module, color);
            Debugger.Loggers.Add(module, debugger);
            result = debugger;
        }
        return result;
    }

    public static void LogToFile(string condition, string stackTrace, LogType type)
    {
    }

    public void Log(object message)
    {
        bool openDebug = this.OpenDebug;
        if (openDebug)
        {
            string text = string.Format("{0} |{1}|<color={2}>{3}|    {4}</color>", new object[]
            {
                DateTime.Now.ToShortTimeString(),
                "INFO",
                this.color,
                this.module,
                message
            });
            Debug.Log(text);
        }
    }

    public void LogError(object message)
    {
        bool openDebug = this.OpenDebug;
        if (openDebug)
        {
            string text = string.Format("{0} |{1}|<color=red>{2}|    {3}</color>", new object[]
            {
                DateTime.Now.ToShortTimeString(),
                "ERROR",
                this.module,
                message
            });
            Debug.LogError(text);
        }
    }

    public void LogException(Exception exception)
    {
        bool openDebug = this.OpenDebug;
        if (openDebug)
        {
            Debug.LogException(exception);
        }
    }

    public void LogWarning(object message)
    {
        bool openDebug = this.OpenDebug;
        if (openDebug)
        {
            string text = string.Format("{0} |{1}|<color=yellow>{2}|    {3}</color>", new object[]
            {
                DateTime.Now.ToShortTimeString(),
                "WARNING",
                this.module,
                message
            });
            Debug.LogWarning(text);
        }
    }

    public void LogFormat(string format, params object[] args)
    {
        bool openDebug = this.OpenDebug;
        if (openDebug)
        {
            string text = string.Format("{0} |{1}|<color=yellow>{2}|    {3}</color>", new object[]
            {
                DateTime.Now.ToShortTimeString(),
                "INFO",
                this.module,
                format
            });
            Debug.LogFormat(text, args);
        }
    }

    public void LogErrorFormat(string format, params object[] args)
    {
        bool openDebug = this.OpenDebug;
        if (openDebug)
        {
            string text = string.Format("{0} |{1}|<color=red>{2}|    {3}</color>", new object[]
            {
                DateTime.Now.ToShortTimeString(),
                "ERROR",
                this.module,
                format
            });
            Debug.LogErrorFormat(text, args);
        }
    }

    public void LogWarningFormat(string format, params object[] args)
    {
        bool openDebug = this.OpenDebug;
        if (openDebug)
        {
            string text = string.Format("{0} |{1}|<color=yellow>{2}|    {3}</color>", new object[]
            {
                DateTime.Now.ToShortTimeString(),
                "WARNING",
                this.module,
                format
            });
            Debug.LogErrorFormat(text, args);
        }
    }
}