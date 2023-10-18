using System.Collections.Generic;
using UnityEngine;
//Receiver
public class TestClassB : MonoBehaviour, IPropInfoHandler<string>
{
    PropertyInfoHandler _propDataBase; // ← プロパティ値プールのDB
    List<string> _propNames = new(); // ← 登録名リスト
    public List<string> PropResisterList => _propNames;
    public PropertyInfoHandler PropHandler => _propDataBase;
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