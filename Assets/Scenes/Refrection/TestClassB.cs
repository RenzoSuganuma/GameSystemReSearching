using System.Collections.Generic;
using UnityEngine;
//Receiver
public class TestClassB : MonoBehaviour
{
    PropertyInfoHandler _propDataBase; // �� �v���p�e�B�l�v�[����DB
    List<string> _propNames = new(); // �� �o�^�����X�g
    private void Start()
    {
        _propDataBase = GetComponent<PropertyInfoHandler>();
        var linker = GameObject.FindAnyObjectByType<PropertyInfoHandlerLinker>();
        _propNames.Add("ClassBTestProp");
        _propDataBase.Resist(_propNames[0], 222);
        linker.ApplyReceiverResisterList(_propNames);
        linker.UpdateReceiverData(_propNames[0], (object)333);
    }
}