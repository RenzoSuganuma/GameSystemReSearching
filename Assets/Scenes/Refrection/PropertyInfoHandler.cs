using UnityEngine;
using DGW;
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