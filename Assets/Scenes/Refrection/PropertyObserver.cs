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
public class PropertyObserver : MonoBehaviour
{
    [SerializeField] DataDictionary<string, object> _dataBase = new();
    private void Awake()
    {
        _dataBase.Add("Test", (object)256);
        _dataBase.Add("Test_", (object)128);
    }
    private void Start()
    {
        Debug.Log($"Data:{_dataBase[(object)256]},{_dataBase["Test"]}");
    }
    private void OnDisable()
    {
        _ = _dataBase;
    }
}