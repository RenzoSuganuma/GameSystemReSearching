using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NameSpaceFirst
{
    public class NameSpaceFunctions : MonoBehaviour
    {
        public void OutPutLog(string name)
        {
            Debug.Log(name);
        }
    }

    public class NameSpaceSubFunctions : MonoBehaviour
    {
        public void OutPutLog(string name)
        {
            Debug.Log(name);
        }
    }
}
