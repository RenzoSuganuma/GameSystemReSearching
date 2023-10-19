using DiscoveryGameWorks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public struct PropInfoCallBackContext
{

}
public interface IPropInfoObserver
{
    PropertyInfoHandlerLinker TargetPropertyInfoHandlerLinker { get; set; }
    List<string> TargetSenderResisterList { get; set; }
    List<string> TargetReceiverResisterList { get; set; }
    Action OnSenderResistersValueChanged { get; set; }
    Action OnReceiverResistersValueChanged { get; set; }
}
public class PropInfoObserver : MonoBehaviour, IPropInfoObserver
{
    public PropertyInfoHandlerLinker TargetPropertyInfoHandlerLinker { get; set; }
    public List<string> TargetSenderResisterList { get; set; } = new();
    public List<string> TargetReceiverResisterList { get; set; } = new();
    public Action OnSenderResistersValueChanged { get; set; } = () => { Debug.Log("センダデータ更新された"); };
    public Action OnReceiverResistersValueChanged { get; set; }
    // temp
    DataDictionary<string , object> tempSenderDataPair = new();
    private void Start()
    {
        TargetPropertyInfoHandlerLinker = GameObject.FindAnyObjectByType<PropertyInfoHandlerLinker>();
        TargetSenderResisterList = TargetPropertyInfoHandlerLinker.SenderResisters;
        TargetReceiverResisterList = TargetPropertyInfoHandlerLinker.ReceiverResisters;
    }
    private void Update()
    {
        foreach (var item in TargetSenderResisterList)
        {
            if (tempSenderDataPair[item] == null)
            {
                tempSenderDataPair.Add(item, TargetPropertyInfoHandlerLinker.ExtractDataFromSender(item));
            }
            Debug.Log($"Linker Data : {item} - {TargetPropertyInfoHandlerLinker.ExtractDataFromSender(item)}");
            Debug.Log($"Observer Data : {item} - {tempSenderDataPair[item]}");
        }
    }
}