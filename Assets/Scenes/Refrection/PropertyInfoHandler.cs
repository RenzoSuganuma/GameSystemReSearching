using UnityEngine;
using DiscoveryGameWorks;
using System.Collections.Generic;
/*
* ����̃C���X�^���X�̃v���p�e�B�l�v�[���N���X
* �̃v���p�e�B���Ď�����N���X
*/
public interface IPropInfoHandler<T>
{
    public List<T> PropResisterList { get; }
    public PropertyInfoHandler PropHandler { get; }
}
public class PropertyInfoHandler : MonoBehaviour
{
    [SerializeField] DataDictionary<string, object> _dataMap = new();
    public DataDictionary<string, object> DataMap => _dataMap;
    public void Resist<T>(string resistName, T value)
    {
        _dataMap.Add(resistName, value);
    }
    public void UnResist(string resistedName)
    {
        _dataMap.Remove(resistedName, _dataMap[resistedName]);
    }
    public bool GetExist(string resistName)
    {
        return _dataMap[resistName] != null;
    }
}