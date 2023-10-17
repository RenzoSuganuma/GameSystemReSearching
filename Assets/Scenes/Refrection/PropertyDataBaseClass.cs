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
public class PropertyDataBaseClass<TDataKey, TDataValue>
{
    //List<DataPair<文字列プロパティ登録名型,object型のプロパティ値保持用の型>>
    //監視対象のプロパティの登録名と名前のデータベース
    DataPair<TDataKey, TDataValue>[] _coreDataBase, _tempDataBase;
    public event Action OnDataAdded;
    public event Action OnDataRemoved;
    public PropertyDataBaseClass()//コンストラクタ
    {
        _coreDataBase = new DataPair<TDataKey, TDataValue>[0];//データベースのインスタンス化
        _tempDataBase = _coreDataBase;
    }
    ~PropertyDataBaseClass()//デコンストラクタ
    {
        _coreDataBase = null;
    }
    public void Add(TDataKey key, TDataValue value)
    {
        var len = _coreDataBase.Length;
        Array.Resize<DataPair<TDataKey, TDataValue>>(ref _coreDataBase, len + 1);
        //Append Element
        _coreDataBase[len] = new DataPair<TDataKey, TDataValue>(key, value);
        if (OnDataAdded != null) OnDataAdded();
    }
    public void Remove(TDataKey key, TDataValue value)
    {
        
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