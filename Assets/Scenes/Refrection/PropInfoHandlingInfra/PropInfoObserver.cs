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
    DataDictionary<string, object> _pastSenderDataPair = new();
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
            if (_pastSenderDataPair[item] == null)
            {
                _pastSenderDataPair.Add(item, TargetPropertyInfoHandlerLinker.ExtractDataFromSender(item));
            }
            Debug.Log($"Linker Data : {item} - {TargetPropertyInfoHandlerLinker.ExtractDataFromSender(item)}");
            Debug.Log($"Observer Data : {item} - {_pastSenderDataPair[item]}");
            Debug.Log($"Data Type Is {_pastSenderDataPair[item].GetType()}");
            if (!TargetPropertyInfoHandlerLinker.ExtractDataFromSender(item).Equals(_pastSenderDataPair[item]))
            {
                OnSenderResistersValueChanged();
                _pastSenderDataPair[item] = TargetPropertyInfoHandlerLinker.ExtractDataFromSender(item);
            }
            //if ((string)TargetPropertyInfoHandlerLinker.ExtractDataFromSender(item) != (string)_pastSenderDataPair[item])
            //{
            //    OnSenderResistersValueChanged();
            //    _pastSenderDataPair[item] = TargetPropertyInfoHandlerLinker.ExtractDataFromSender(item);
            //}else
            //{
            //    Debug.Log("データが等しい！");
            //}
        } // Compare Data Between This Obeserver To Linker Property Info
    }
}
// (object)int -> System.Int32 : (object)float -> System.Single : (object)string -> System.String