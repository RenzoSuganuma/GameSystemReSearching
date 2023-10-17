using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DiscoveryGameWorks;
public class PropertyInfoHandlerLinker : MonoBehaviour
{
    [SerializeField] PropertyInfoHandler _propInfoHandler; // プロパティ参照元
    [SerializeField] PropertyInfoHandler _targetChangedPropInfoHandler; // プロパティ参照値の初期化先
    List<string> _propResisterName = new();
    private void Update()
    {
        Debug.Log(ExtractData(0));
    }
    /// <summary> 登録名リストの登録 </summary>
    /// <param name="resisterNames"></param>
    public void ApplyResisterList(List<string> resisterNames)
    {
        _propResisterName = resisterNames;
    }
    /// <summary> データベースからのデータの抽出 </summary>
    /// <param name="resisterIndex"></param>
    /// <returns></returns>
    public object ExtractData(int resisterIndex)
    {
        return _propInfoHandler.DataMap[_propResisterName[resisterIndex]];
    }
}