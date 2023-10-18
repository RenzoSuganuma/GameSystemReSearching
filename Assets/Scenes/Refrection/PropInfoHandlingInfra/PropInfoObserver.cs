using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public interface IPropInfoObserver
{
    PropertyInfoHandlerLinker PropertyInfoHandlerLinker { get; set; }
    List<string> ObserveTargetResistList { get; set; }
}
public class PropInfoObserver : MonoBehaviour, IPropInfoObserver
{
    public PropertyInfoHandlerLinker PropertyInfoHandlerLinker { get; set; }
    public List<string> ObserveTargetResistList { get; set; } = new();
    private void Update()
    {
        
    }
    public void PropInfoObserverSetup(PropertyInfoHandlerLinker propInfoHandlerLinker, List<string> observeTargetResistList)
    {
        this.PropertyInfoHandlerLinker = propInfoHandlerLinker;
        this.ObserveTargetResistList = observeTargetResistList;
    }
}