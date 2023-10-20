using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DemoPIHUserA : PropInfoUser, IPropInfoUser
{
    public PropertyInfoHandlerLinker PropInfoHandlerLinker { get; set; }
    public PropertyInfoHandler PropInfoHandler { get; set; }
    public List<string> ResisterNameList { get; set; } = new();
    [SerializeField] Text _text;
    int _integer = 0;
    protected override void SetUpPropInfoUser()
    {
        PropInfoHandlerLinker = GameObject.FindAnyObjectByType<PropertyInfoHandlerLinker>();
        PropInfoHandler = GetComponent<PropertyInfoHandler>();
    }
    private void Start()
    {
        SetUpPropInfoUser();
        ResisterNameList.Add("Test Propery Data From User A");
        PropInfoHandler.Resist(ResisterNameList[0], _integer);
        PropInfoHandlerLinker.ApplySenderResisterList(ResisterNameList);
    }
    private void Update()
    {
        PropInfoHandlerLinker.UpdateSenderData(ResisterNameList[0], _integer);
        _text.text = _integer.ToString();
    }
    public void IncrementInt()
    {
        _integer++;
    }
}
