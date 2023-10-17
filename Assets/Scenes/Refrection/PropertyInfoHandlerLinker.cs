using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DiscoveryGameWorks;
public class PropertyInfoHandlerLinker : MonoBehaviour
{
    /// <summary> �v���p�e�B�Q�ƌ��̏��n���h���[ </summary>
    [SerializeField] PropertyInfoHandler _sender; // �v���p�e�B�Q�ƌ�
    /// <summary> �v���p�e�B�Q�ƌ��̃f�[�^�̏�������i�^�[�Q�b�g�j </summary>
    [SerializeField] PropertyInfoHandler _receiver; // �v���p�e�B�Q�ƒl�̏�������
    List<string> _senderResisters = new();
    List<string> _receiverResisters = new();
    private void Update()
    {
        Debug.Log($"ClassA->{ExtractDataFromSender("ClassATestProp")}");
        Debug.Log($"ClassB->{ExtractDataFromReceiver("ClassBTestProp")}");
    }
    /// <summary> �o�^�����X�g�̓o�^ </summary>
    /// <param name="resisterNames"></param>
    public void ApplySenderResisterList(List<string> resisterNames) // �Q�ƌ�����Ăяo�����
    {
        _senderResisters = resisterNames;
    }public void ApplyReceiverResisterList(List<string> resisterNames) // �Q�ƌ�����Ăяo�����
    {
        _receiverResisters = resisterNames;
    }
    #region ���ʕ�
    /// <summary> �o�^�����l�̍X�V </summary>
    /// <param name="resisterName"></param>
    /// <param name="value"></param>
    void UpdateData(PropertyInfoHandler propHandler, string resisterName, object value) // �v���p�e�B�Q�ƌ��ɓo�^����Ă���o�^���ɑΉ������l�̍X�V
    {
        propHandler.DataMap[resisterName] = value;
    }
    /// <summary> �f�[�^�x�[�X����̃f�[�^�̒��o </summary>
    /// <param name="resisterIndex"></param>
    /// <returns></returns>
    object ExtractData(PropertyInfoHandler propHandler, string resisterName)
    {
        return propHandler.DataMap[resisterName];
    }
    #endregion
    #region �v���p�e�B���Q�ƌ�
    public void SendDataToSender(string targetResistName, object passingData) // �w�肵����������̓o�^���֎w�肵���f�[�^�𑗂�
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
    #region �v���p�e�B��񏉊�����
    public void SendDataToReceiver(string targetResistName, object passingData) // �w�肵����������̓o�^���֎w�肵���f�[�^�𑗂�
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