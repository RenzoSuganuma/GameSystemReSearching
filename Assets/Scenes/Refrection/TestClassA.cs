using System.Collections.Generic;
using UnityEngine;
//Sender
public class TestClassA : PropInfoUser, IPropInfoUser
{
    public PropertyInfoHandlerLinker PropertyInfoHandlerLinker { get; set; }
    public PropertyInfoHandler PropertyInfoHandler { get;set; }
    public List<string> ResisterNameList { get; set; } = new();
    protected override void SetUpPropInfoUser()
    {
        PropertyInfoHandler = GetComponent<PropertyInfoHandler>();
        PropertyInfoHandlerLinker = GameObject.FindAnyObjectByType<PropertyInfoHandlerLinker>();
    }
    private void Start()
    {
        SetUpPropInfoUser();
        ResisterNameList.Add("ClassATestProp");
        PropertyInfoHandler.Resist(ResisterNameList[0], 1.0f);
        PropertyInfoHandlerLinker.ApplySenderResisterList(ResisterNameList);
        PropertyInfoHandlerLinker.UpdateSenderData(ResisterNameList[0], 222);
    }
}