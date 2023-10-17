using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
//Receiver
public class TestClassB : MonoBehaviour
{
    PropertyInfoHandler _propDataBase; // ← プロパティ値プールのDB
    List<string> _propNames = new(); // ← 登録名リスト
    public List<string> PropResisterList => _propNames;
    public PropertyInfoHandler PropHandler => _propDataBase;
    private void Start()
    {
        _propDataBase = GetComponent<PropertyInfoHandler>();
        _propDataBase.Resist("ClassBTestProp", 1.0f);
        _propNames.Add("ClassBTestProp");
        var linker =
        GameObject.FindAnyObjectByType
            <PropertyInfoHandlerLinker>();
        linker.ApplyResisterList(_propNames);
    }
}
