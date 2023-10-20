using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TestClassC : MonoBehaviour, IPropInfoObserver
{
    PropInfoObserver _obs;
    private void Awake()
    {
        _obs = GameObject.FindAnyObjectByType<PropInfoObserver>();
    }
    private void OnEnable()
    {
        _obs.OnSenderDataHasChanged += OnSenderPropertyValueChanged;
        _obs.OnReceiverDataHasChanged += OnReciverPropertyValueChanged;
    }
    private void OnDisable()
    {
        _obs.OnSenderDataHasChanged -= OnSenderPropertyValueChanged;
        _obs.OnReceiverDataHasChanged -= OnReciverPropertyValueChanged;
    }
    public void OnReciverPropertyValueChanged(PropInfoCallBackContext context)
    {
        Debug.Log("利用部クラス C " + context.ToString());
    }
    public void OnSenderPropertyValueChanged(PropInfoCallBackContext context)
    {
        Debug.Log("利用部クラス C " + context.ToString());
    }
}