using DiscoveryGameWorks;
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
        _propDataBase.Resist("ClassATestProp", 1.0f);
        _propNames.Add("ClassATestProp");
        var linker =
        GameObject.FindAnyObjectByType
            <PropertyInfoHandlerLinker>();
        linker.ApplyResisterList(_propNames);
    }
}