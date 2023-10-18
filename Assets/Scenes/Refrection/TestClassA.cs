using System.Collections.Generic;
using UnityEngine;
//Sender
public class TestClassA : MonoBehaviour, IPropInfoHandler<string>
{
    PropertyInfoHandler _propDataBase; // ← プロパティ値プールのDB
    List<string> _propNames = new(); // ← 登録名リスト
    public List<string> PropResisterList => _propNames;
    public PropertyInfoHandler PropHandler => _propDataBase;
    private void Start()
    {
        _propDataBase = GetComponent<PropertyInfoHandler>();
        var linker = GameObject.FindAnyObjectByType<PropertyInfoHandlerLinker>();
        _propNames.Add("ClassATestProp");
        _propDataBase.Resist(_propNames[0], 1.0f);
        linker.ApplySenderResisterList(_propNames);
        linker.UpdateSenderData(_propNames[0], (object)222);
    }
}