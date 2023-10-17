using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DiscoveryGameWorks;
public class PropertyInfoHandlerLinker : MonoBehaviour
{
    [SerializeField] PropertyInfoHandler _propInfoHandler; // �v���p�e�B�Q�ƌ�
    [SerializeField] PropertyInfoHandler _targetChangedPropInfoHandler; // �v���p�e�B�Q�ƒl�̏�������
    List<string> _propResisterName = new();
    private void Update()
    {
        Debug.Log(ExtractData(0));
    }
    /// <summary> �o�^�����X�g�̓o�^ </summary>
    /// <param name="resisterNames"></param>
    public void ApplyResisterList(List<string> resisterNames)
    {
        _propResisterName = resisterNames;
    }
    /// <summary> �f�[�^�x�[�X����̃f�[�^�̒��o </summary>
    /// <param name="resisterIndex"></param>
    /// <returns></returns>
    public object ExtractData(int resisterIndex)
    {
        return _propInfoHandler.DataMap[_propResisterName[resisterIndex]];
    }
}