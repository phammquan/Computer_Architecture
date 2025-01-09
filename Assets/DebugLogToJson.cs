using System.Collections.Generic;
using System.IO;
using Oculus.Interaction;
using UnityEngine;

public class DebugLogToJson : MonoBehaviour
{
    private Grabbable _grabbable;
    private CustomLogger _customLogger;
    private string _logFilePath;

    void Start()
    {
        _grabbable = GetComponent<Grabbable>();
        _customLogger = gameObject.AddComponent<CustomLogger>();
        _logFilePath = Path.Combine(Application.persistentDataPath, "log.json");

        _grabbable.WhenPointerEventRaised += OnGrab;
    }

    private void OnGrab(PointerEvent evt)
    {
        if (evt.Type == PointerEventType.Select)
        {
            Debug.Log("GameObject grabbed");
            SaveLogToFile();
        }
    }

    private void SaveLogToFile()
    {
        string logContent = _customLogger.ReadLogsFromFile();
        if (logContent != null)
        {
            File.WriteAllText(_logFilePath, logContent);
            Debug.Log("Log file saved to: " + _logFilePath);
            Debug.Log("Log file path: " + _logFilePath);
        }
        else
        {
            Debug.Log("Log file not found.");
        }
    }
}