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
        _dataBase.Add("Test", (object)100);
        _dataBase.Add("Test_", (object)100);
    }
    private void Start()
    {
        //_dataBase.Remove("Test", (object)100);
    }
    private void OnDisable()
    {
        _ = _dataBase;
    }
}