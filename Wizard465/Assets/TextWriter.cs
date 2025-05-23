using System;
using System.IO;
using UnityEngine;

public class TextWriter : MonoBehaviour
{
    private const string DEFAULT_LOG_FILE_NAME = "wizard_465_log.txt"; // Default log file name if none is provided 

    public string logFileName; // Public property to allow setting the log file name from outside the class

    private string _fileName;

    public string fileName
    {
        get
        {
            if (string.IsNullOrEmpty(_fileName))
            {
                DateTime now = DateTime.Now; // Get the current date and time   
                string timestamp = now.ToString("yyyyMMdd_HHmmss"); // Format the timestamp as needed (e.g., "yyyyMMdd_HHmmss") 
                // Set the file name if it hasn't been set yet
                if (!string.IsNullOrEmpty(logFileName))
                {
                    _fileName = string.Format("{0}_{1}", timestamp, logFileName); // Create the log file name with the timestamp
                }
                else
                {
                    _fileName = string.Format("{0}_{1}", timestamp, DEFAULT_LOG_FILE_NAME); // Create the log file name with the timestamp
                }
            }
            return _fileName;
        }
        set
        {
            _fileName = value; // Allow setting the file name if needed 
        }
    }

    public string filePath
    {
        get
        {
            string logFilePath = Path.Combine(Application.persistentDataPath, this.fileName);
            return logFilePath;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // initialize the file name when the script starts  
        string fileName = this.fileName;
        Debug.Log("Log file name: " + fileName); // Log the file path for debugging purposes
        Debug.Log("Log file path: " + this.filePath); // Log the file path for debugging purposes
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LogData(DateTime timestamp, string name)
    {
        LogData(timestamp, name, ""); // Overload method to handle the case when no data is provided, just the name 
    }

    public void LogData(DateTime timestamp, string name, string data)
    {
        string lineToWrite = string.Format("{0} - {1}: {2}\n", timestamp.ToString("yyyy-MM-dd HH:mm:ss"), name, data);
        writeTextToLog(lineToWrite); // Call the method to write the line to the log file   
    }

    private void writeTextToLog(string toWrite)
    {
        File.AppendAllText(this.filePath, toWrite);
    }

    public void AppendAllText(string toWrite)
    {
        File.AppendAllText(this.filePath, toWrite);
    }

    public void WriteAllText(string toWrite)
    {
        File.WriteAllText(this.filePath, toWrite);
    }
}
