using DiscoveryGameWorks;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public struct PropInfoCallBackContext // ← どのレジスタ名のデータが変化したかの構造体
{
    public string _resisterName;
    public object _resisterData;
    public Type _resisterDataType;
    public PropInfoCallBackContext(string resisterName, object resisterData)
    {
        _resisterData = resisterData;
        _resisterName = resisterName;
        _resisterDataType = resisterData.GetType();
    }
    public override string ToString()
    {
        return $"[{_resisterName} : {_resisterData} : {_resisterDataType}]";
    }
}
public interface IPropInfoObserver
{
    void OnSenderPropertyValueChanged(PropInfoCallBackContext context);
    void OnReciverPropertyValueChanged(PropInfoCallBackContext context);
}
public class PropInfoObserver : MonoBehaviour
{
    public PropertyInfoHandlerLinker TargetPropertyInfoHandlerLinker { get; set; }
    public List<string> TargetSenderResisterList { get; set; } = new();
    public List<string> TargetReceiverResisterList { get; set; } = new();
    public delegate void ChangedValueDalegate(PropInfoCallBackContext context);
    public event ChangedValueDalegate OnSenderResistersValueChanged;
    public event ChangedValueDalegate OnReceiverResistersValueChanged;
    // Use For Compare Data
    DataDictionary<string, object> _pastSenderDataPair = new();
    DataDictionary<string, object> _pastReceiverDataPair = new();
    private void Start()
    {
        TargetPropertyInfoHandlerLinker = GameObject.FindAnyObjectByType<PropertyInfoHandlerLinker>();
        TargetSenderResisterList = TargetPropertyInfoHandlerLinker.SenderResisters;
        TargetReceiverResisterList = TargetPropertyInfoHandlerLinker.ReceiverResisters;
    }
    private void Update()
    {
        CheckSenderDataChange();
        CheckReceiverDataChange();
    }
    private void CheckSenderDataChange()
    {
        foreach (var item in TargetSenderResisterList) // <- SENDER
        {
            if (_pastSenderDataPair[item] == null)
            {
                _pastSenderDataPair.Add(item, TargetPropertyInfoHandlerLinker.ExtractDataFromSender(item));
            }
            if (!TargetPropertyInfoHandlerLinker.ExtractDataFromSender(item).Equals(_pastSenderDataPair[item]))
            {
                var cntxt = new PropInfoCallBackContext(item, TargetPropertyInfoHandlerLinker.ExtractDataFromSender(item));
                //OnSenderResistersValueChanged.Invoke(cntxt);
                OnSenderResistersValueChanged(cntxt);
                _pastSenderDataPair[item] = TargetPropertyInfoHandlerLinker.ExtractDataFromSender(item);
            }
        } // Compare Data Between This Obeserver To Linker Property Info
    }
    private void CheckReceiverDataChange()
    {
        foreach (var item in TargetReceiverResisterList) // <- RECEIVER
        {
            if (_pastReceiverDataPair[item] == null)
            {
                _pastReceiverDataPair.Add(item, TargetPropertyInfoHandlerLinker.ExtractDataFromReceiver(item));
            }
            if (!TargetPropertyInfoHandlerLinker.ExtractDataFromReceiver(item).Equals(_pastReceiverDataPair[item]))
            {
                var cntxt = new PropInfoCallBackContext(item, TargetPropertyInfoHandlerLinker.ExtractDataFromReceiver(item));
                //OnReceiverResistersValueChanged.Invoke(cntxt);
                OnReceiverResistersValueChanged(cntxt);
                _pastReceiverDataPair[item] = TargetPropertyInfoHandlerLinker.ExtractDataFromReceiver(item);
            }
        } // Compare Data Between This Obeserver To Linker Property Info
    }
}
// (object)int -> System.Int32 : (object)float -> System.Single : (object)string -> System.String