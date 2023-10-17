using UnityEngine;
using DiscoveryGameWorks;
/*
* 特定のインスタンスのプロパティ値プールクラス
* のプロパティを監視するクラス
*/
public class PropertyInfoHandler : MonoBehaviour
{
    [SerializeField] DataDictionary<string, object> _dataMap = new();
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
    private void Awake()
    {
        Resist("TestData_", 256);
        Debug.Log($"{_dataMap["TestData_"]}");
    }
    private void Start()
    {
        UnResist("TestData_");
    }
}