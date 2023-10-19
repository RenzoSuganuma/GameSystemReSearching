using System.Collections.Generic;
using UnityEngine;
public interface IPropInfoUser
{
    PropertyInfoHandlerLinker PropInfoHandlerLinker { get; set; }
    PropertyInfoHandler PropInfoHandler { get; set; }
    List<string> ResisterNameList { get; set; }
}
[RequireComponent(typeof(PropertyInfoHandler))]
/// <summary> �v���p�e�B���C���t�����p�� </summary>
public abstract class PropInfoUser : MonoBehaviour
{
    protected abstract void SetUpPropInfoUser();
}