using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
namespace RuntimeLog
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
}
