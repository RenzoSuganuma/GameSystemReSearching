using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropInfoObserveUser : MonoBehaviour, IPropInfoObserver
{
    [SerializeField] PropInfoObserver observer;
    private void OnEnable()
    {
        observer.OnSenderResistersValueChanged += OnSenderPropertyValueChanged;
        observer.OnReceiverResistersValueChanged += OnReciverPropertyValueChanged;
    }
    private void OnDisable()
    {
        observer.OnSenderResistersValueChanged -= OnSenderPropertyValueChanged;
        observer.OnReceiverResistersValueChanged -= OnReciverPropertyValueChanged;
    }
    public void OnReciverPropertyValueChanged(PropInfoCallBackContext context)
    {
        Debug.Log($"���V�[�o�[�f�[�^ : " + context.ToString());
    }
    public void OnSenderPropertyValueChanged(PropInfoCallBackContext context)
    {
        Debug.Log($"�Z���_�f�[�^ : " + context.ToString());
    }
}
