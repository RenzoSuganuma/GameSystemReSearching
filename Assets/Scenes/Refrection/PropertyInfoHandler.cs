using UnityEngine;
using DiscoveryGameWorks;
using System.Collections.Generic;
/*
* ����̃C���X�^���X�̃v���p�e�B�l�v�[���N���X
* �̃v���p�e�B���Ď�����N���X
*/
/// <summary> �v���p�e�B���n���h���[�Ƃ��̃����J�̗��p�����p������C���^�[�t�F�[�X </summary>
/// <typeparam name="T"></typeparam>
public interface IPropInfoHandler<T>
{
    public List<T> PropResisterList { get; }
    public PropertyInfoHandler PropHandler { get; }
}
/// <summary> ���W�X�^�ƃf�[�^�̃y�A����Ȃ�f�[�^�\���ŁA
/// ���W�X�^���ɉ������f�[�^�����߂Ă����f�[�^�x�[�X </summary>
public class PropertyInfoHandler : MonoBehaviour
{
    [SerializeField] DataDictionary<string, object> _dataMap = new();
    public DataDictionary<string, object> DataMap => _dataMap;
    /// <summary> ���W�X�^���ƃf�[�^�̓o�^ </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="resistName"></param>
    /// <param name="value"></param>
    public void Resist<T>(string resistName, T value)
    {
        _dataMap.Add(resistName, value);
    }
    /// <summary> ���W�X�^�����w�肵�āA ���̃��W�X�^���ɂ������o�^�����f�[�^�x�[�X�������</summary>
    /// <param name="resistedName"></param>
    public void UnResist(string resistedName)
    {
        _dataMap.Remove(resistedName, _dataMap[resistedName]);
    }
    /// <summary> �w�肵�����W�X�^���ɑΉ������f�[�^�y�A���f�[�^�x�[�X�ɑ��݂��邩�������� </summary>
    /// <param name="resistName"></param>
    /// <returns></returns>
    public bool GetExist(string resistName)
    {
        return _dataMap[resistName] != null;
    }
}