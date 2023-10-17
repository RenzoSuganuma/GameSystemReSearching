using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DGW;
using Unity.VisualScripting;
/*
* ����̃C���X�^���X�̃v���p�e�B�l�v�[���N���X
* �̃v���p�e�B���Ď�����N���X
*/
public class PropertyInfoHandler : MonoBehaviour
{
    [SerializeField] DataDictionary<string, object> _dataBase = new();
    public void Resist<T>(string resistName, T value)
    {
        _dataBase.Add(resistName, value);
    }
    public void UnResist(string resistedName)
    {
        _dataBase.Remove(resistedName, _dataBase[resistedName]);
    }
    private void Awake()
    {
        Resist<int>("TestData", 0);
    }
    private void Start()
    {
        UnResist("TestData");
    }
}