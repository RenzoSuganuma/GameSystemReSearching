using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
//Sender
public class TestClassA : PropInfoUser, IPropInfoUser
{
    public PropertyInfoHandlerLinker PropertyInfoHandlerLinker { get; set; }
    public PropertyInfoHandler PropertyInfoHandler { get;set; }
    public List<string> ResisterNameList { get; set; } = new();
    private void Start()
    {
        PropertyInfoHandler = GetComponent<PropertyInfoHandler>();
        PropertyInfoHandlerLinker = GameObject.FindAnyObjectByType<PropertyInfoHandlerLinker>();
        ResisterNameList.Add("ClassATestProp");
        PropertyInfoHandler.Resist(ResisterNameList[0], 1.0f);
        PropertyInfoHandlerLinker.ApplySenderResisterList(ResisterNameList);
        PropertyInfoHandlerLinker.UpdateSenderData(ResisterNameList[0], 222);
    }
}