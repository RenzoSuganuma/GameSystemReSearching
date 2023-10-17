using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DGW;
using Unity.VisualScripting;
/*
* 特定のインスタンスのプロパティ値プールクラス
* のプロパティを監視するクラス
*/
public class PropertyObserver : MonoBehaviour
{
    [SerializeField] PropertyDataBaseClass<string, object> _dataBase = new();
    private void Start()
    {
        _dataBase.Add("Test", (object)100);
    }
}