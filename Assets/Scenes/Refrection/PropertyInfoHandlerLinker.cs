using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DiscoveryGameWorks;
public class PropertyInfoHandlerLinker : MonoBehaviour
{
    /// <summary> プロパティ参照元の情報ハンドラー </summary>
    [SerializeField] PropertyInfoHandler _sender; // プロパティ参照元
    /// <summary> プロパティ参照元のデータの初期化先（ターゲット） </summary>
    [SerializeField] PropertyInfoHandler _receiver; // プロパティ参照値の初期化先
    List<string> _senderResisters = new();
    List<string> _receiverResisters = new();
    private void Update()
    {
        Debug.Log($"ClassA->{ExtractDataFromSender("ClassATestProp")}");
        Debug.Log($"ClassB->{ExtractDataFromReceiver("ClassBTestProp")}");
    }
    /// <summary> 登録名リストの登録 </summary>
    /// <param name="resisterNames"></param>
    public void ApplySenderResisterList(List<string> resisterNames) // 参照元から呼び出される
    {
        _senderResisters = resisterNames;
    }public void ApplyReceiverResisterList(List<string> resisterNames) // 参照元から呼び出される
    {
        _receiverResisters = resisterNames;
    }
    #region 共通部
    /// <summary> 登録した値の更新 </summary>
    /// <param name="resisterName"></param>
    /// <param name="value"></param>
    void UpdateData(PropertyInfoHandler propHandler, string resisterName, object value) // プロパティ参照元に登録されている登録名に対応した値の更新
    {
        propHandler.DataMap[resisterName] = value;
    }
    /// <summary> データベースからのデータの抽出 </summary>
    /// <param name="resisterIndex"></param>
    /// <returns></returns>
    object ExtractData(PropertyInfoHandler propHandler, string resisterName)
    {
        return propHandler.DataMap[resisterName];
    }
    #endregion
    #region プロパティ情報参照元
    public void SendDataToSender(string targetResistName, object passingData) // 指定した初期化先の登録名へ指定したデータを送る
    {
        if (_sender.DataMap.Find(targetResistName) != null)
        {
            _sender.DataMap[targetResistName] = passingData;
        }
        else
        {
            _sender.Resist(targetResistName, passingData);
        }
    }
    /// <summary>  </summary>
    /// <param name="resisterName"></param>
    /// <param name="value"></param>
    public void UpdateSenderData(string resisterName, object value)
    {
        UpdateData(_sender, resisterName, value);
    }
    public object ExtractDataFromSender(string resisterName)
    {
        return ExtractData(_sender, resisterName);
    }
    #endregion
    #region プロパティ情報初期化先
    public void SendDataToReceiver(string targetResistName, object passingData) // 指定した初期化先の登録名へ指定したデータを送る
    {
        if (_receiver.DataMap.Find(targetResistName) != null)
        {
            _receiver.DataMap[targetResistName] = passingData;
        }
        else
        {
            _receiver.Resist(targetResistName, passingData);
        }
    }
    public void UpdateReceiverData(string resisterName, object value)
    {
        UpdateData(_receiver, resisterName, value);
    }
    public object ExtractDataFromReceiver(string resisterName)
    {
        return ExtractData(_receiver, resisterName);
    }
    #endregion
}