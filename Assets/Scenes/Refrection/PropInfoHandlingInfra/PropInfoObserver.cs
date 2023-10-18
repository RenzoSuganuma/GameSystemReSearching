using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public interface IPropInfoObserver
{
    PropertyInfoHandlerLinker PropertyInfoHandlerLinker { get; set; }
    List<string> ObserveTargetResistList { get; set; }
}
public class PropInfoObserver : MonoBehaviour
{
    
}
