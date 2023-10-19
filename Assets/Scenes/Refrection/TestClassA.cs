using System.Collections.Generic;
using UnityEngine;
//Sender
public class TestClassA : PropInfoUser, IPropInfoUser
{
    public PropertyInfoHandlerLinker PropInfoHandlerLinker { get; set; }
    public PropertyInfoHandler PropInfoHandler { get; set; }
    public List<string> ResisterNameList { get; set; } = new();
    //temp 
    int tmp = 0;
    private void Start()
    {
        SetUpPropInfoUser();
        ResisterNameList.Add("ClassATestProp");
        PropInfoHandler.Resist(ResisterNameList[0], 1.0f);
        PropInfoHandlerLinker.ApplySenderResisterList(ResisterNameList);
        PropInfoHandlerLinker.UpdateSenderData(ResisterNameList[0], 0);
    }
    private void Update()
    {
        PropInfoHandlerLinker.UpdateSenderData(ResisterNameList[0], tmp);
        if (tmp < 1) tmp++;
    }
    protected override void SetUpPropInfoUser()
    {
        PropInfoHandler = GetComponent<PropertyInfoHandler>();
        PropInfoHandlerLinker = GameObject.FindAnyObjectByType<PropertyInfoHandlerLinker>();
    }
}