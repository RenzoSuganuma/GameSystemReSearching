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
    [SerializeField] PropertyDataBaseClass<string, object> _dataBase = new();
    private void Start()
    {
        _dataBase.Add("Test", (object)100);
    }
}