using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CustomLogger : MonoBehaviour
{
    private List<LogEntry> _logEntries = new List<LogEntry>();
    private string _logFilePath;

    void OnEnable()
    {
        _logFilePath = Path.Combine(Application.persistentDataPath, "log.json");
        Application.logMessageReceived += HandleLog;
    }

    void OnDisable()
    {
        Application.logMessageReceived -= HandleLog;
    }

    void HandleLog(string logString, string stackTrace, LogType type)
    {
        LogEntry logEntry = new LogEntry(logString, stackTrace, type);
        _logEntries.Add(logEntry);
        SaveLogsToFile();
    }

    void SaveLogsToFile()
    {
        string json = JsonUtility.ToJson(new { logs = _logEntries }, true);
        File.WriteAllText(_logFilePath, json);
    }

    public string ReadLogsFromFile()
    {
        if (File.Exists(_logFilePath))
        {
            return File.ReadAllText(_logFilePath);
        }
        return null;
    }
}

public class LogEntry
{
    public string Message;
    public string StackTrace;
    public LogType LogType;
    public DateTime Timestamp;

    public LogEntry(string message, string stackTrace, LogType logType)
    {
        Message = message;
        StackTrace = stackTrace;
        LogType = logType;
        Timestamp = DateTime.Now;
    }
}