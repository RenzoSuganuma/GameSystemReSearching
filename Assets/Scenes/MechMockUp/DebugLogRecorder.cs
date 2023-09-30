using UnityEngine;
using System.IO;
using System;
namespace DebugLogRecorder
{
    public class RuntimeLogComponent
    {
        Rect _logRect;
        public RuntimeLogComponent(Rect rect)
        {
            _logRect = rect;
        }
        public void DisplayLog(string logText)
        {
            GUI.Box(_logRect, logText);
        }
    }
    [Serializable]
    public class GameDebugLogRecordFunction
    {
        static string _dataPath = string.Empty;
        public GameDebugLogRecordFunction(string filename)
        {
            _dataPath = Application.dataPath + "\\" + filename + ".txt";
        }
        public void FileOut(string streamString)
        {
            StreamWriter sw = new StreamWriter(_dataPath, true);
            sw.WriteLine(streamString);
            sw.Flush();
            sw.Close();
        }
    }
}
