using System.Collections.Generic;
using UnityEngine;
public interface IPropInfoUser
{
    PropertyInfoHandlerLinker PropertyInfoHandlerLinker { get; set; }
    PropertyInfoHandler PropertyInfoHandler { get; set; }
    List<string> ResisterNameList { get; set; }
}
[RequireComponent(typeof(PropertyInfoHandler))]
/// <summary> プロパティ情報インフラ利用部 </summary>
public class PropInfoUser : MonoBehaviour { }