using UnityEngine;
using DiscoveryGameWorks;
using System.Collections.Generic;
/*
* 特定のインスタンスのプロパティ値プールクラス
* のプロパティを監視するクラス
*/
/// <summary> プロパティ情報ハンドラーとそのリンカの利用部が継承するインターフェース </summary>
/// <typeparam name="T"></typeparam>
public interface IPropInfoHandler<T>
{
    public List<T> PropResisterList { get; }
    public PropertyInfoHandler PropHandler { get; }
}
/// <summary> レジスタとデータのペアからなるデータ構造で、
/// レジスタ名に応じたデータをためておくデータベース </summary>
public class PropertyInfoHandler : MonoBehaviour
{
    [SerializeField] DataDictionary<string, object> _dataMap = new();
    public DataDictionary<string, object> DataMap => _dataMap;
    /// <summary> レジスタ名とデータの登録 </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="resistName"></param>
    /// <param name="value"></param>
    public void Resist<T>(string resistName, T value)
    {
        _dataMap.Add(resistName, value);
    }
    /// <summary> レジスタ名を指定して、 そのレジスタ名にあった登録情報をデータベースから消す</summary>
    /// <param name="resistedName"></param>
    public void UnResist(string resistedName)
    {
        _dataMap.Remove(resistedName, _dataMap[resistedName]);
    }
    /// <summary> 指定したレジスタ名に対応したデータペアがデータベースに存在するか検索する </summary>
    /// <param name="resistName"></param>
    /// <returns></returns>
    public bool GetExist(string resistName)
    {
        return _dataMap[resistName] != null;
    }
}