using System.Collections.Generic;
using UnityEngine;
//Receiver
public class TestClassB : PropInfoUser, IPropInfoUser
{
    public PropertyInfoHandlerLinker PropInfoHandlerLinker { get; set; }
    public PropertyInfoHandler PropInfoHandler { get; set; }
    public List<string> ResisterNameList { get; set; } = new();
    //temp 
    string tmp = "temp";
    private void Start()
    {
        SetUpPropInfoUser();
        ResisterNameList.Add("ClassBTestProp");
        PropInfoHandler.Resist(ResisterNameList[0], 1);
        PropInfoHandlerLinker.ApplyReceiverResisterList(ResisterNameList);
        PropInfoHandlerLinker.UpdateReceiverData(ResisterNameList[0], 0);
    }
    private void Update()
    {
        PropInfoHandlerLinker.UpdateReceiverData(ResisterNameList[0], tmp);
        if (tmp != "Prop") tmp = "Prop";
    }
    protected override void SetUpPropInfoUser()
    {
        PropInfoHandler = GetComponent<PropertyInfoHandler>();
        PropInfoHandlerLinker = GameObject.FindAnyObjectByType<PropertyInfoHandlerLinker>();
    }
}