using System.Collections.Generic;
using UnityEngine;
using DGW;
using System;
using System.Collections;
/*
* 
*/
[Serializable]
/// <summary> 特定の登録名に対応した値を格納するための機能を提供するクラス </summary>
public class DataDictionary<TDataKey, TDataValue>
{
    //List<DataPair<文字列プロパティ登録名型,object型のプロパティ値保持用の型>>
    //監視対象のプロパティの登録名と名前のデータベース
    DataPair<TDataKey, TDataValue>[] _coreDataBase;
    public event Action OnDataAdded;
    public event Action OnDataRemoved;
    public DataDictionary()//コンストラクタ
    {
        _coreDataBase = new DataPair<TDataKey, TDataValue>[0];//データベースのインスタンス化
    }
    ~DataDictionary()//デコンストラクタ
    {
        _coreDataBase = null;
        OnDataAdded = null;
        OnDataRemoved = null;
    }
    /// <summary> データの登録 </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    public void Add(TDataKey key, TDataValue value)
    {
        for (int i = 0; i < _coreDataBase.Length; i++)
        {
            if (_coreDataBase[i].Key.Equals(key))
            {
                throw new Exception("It Seems Already Appended Same Key Value");
            }
        }//Search Same Key Value
        Array.Resize<DataPair<TDataKey, TDataValue>>(ref _coreDataBase, _coreDataBase.Length + 1);
        //Append Element
        _coreDataBase[_coreDataBase.Length - 1] = new DataPair<TDataKey, TDataValue>(key, value);
        if (OnDataAdded != null) OnDataAdded();
    }
    /// <summary> データの登録解除 </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    public void Remove(TDataKey key, TDataValue value)
    {
        for (int i = 0; i < _coreDataBase.Length; i++)
        {
            if (_coreDataBase[i].Key.Equals(key)//If The Pair Found
                && _coreDataBase[i].Value.Equals(value))
            {
                _coreDataBase[i] = null;//Erase Element
                for (int j = i; j < _coreDataBase.Length; j++)
                {
                    _coreDataBase[j] = _coreDataBase[(j + 1 < _coreDataBase.Length) ? j + 1 : j];
                    _coreDataBase[(j + 1 < _coreDataBase.Length) ? j + 1 : j] = null;
                    Array.Resize<DataPair<TDataKey, TDataValue>>(ref _coreDataBase, _coreDataBase.Length - 1);
                }//Make No Gap
            }
        }//Serach Pairs
        if (OnDataAdded != null) OnDataRemoved();
    }
}
/// <summary> 
/// Dictionaryみたいなデータペアのクラス。
/// Listとの併用を想定して設計している。
/// </summary>
/// <typeparam name="TKey"></typeparam>
/// <typeparam name="TValue"></typeparam>
[Serializable]
public class DataPair<TKey, TValue>
{
    TKey _key;
    public TKey Key => _key;
    TValue _value;
    public TValue Value => _value;
    /// <summary> 文字列型のKeyの値 </summary>
    public string SKey => _key.ToString();
    /// <summary> 文字列型のValueの値 </summary>
    public string SValue => _value.ToString();
    public DataPair(TKey key, TValue value)
    {
        _key = key;
        _value = value;
    }
    /// <summary> Keyの型を返す </summary>
    public Type KeyType => typeof(TKey);
    /// <summary> Valueの型を返す </summary>
    public Type ValueType => typeof(TValue);
}