using System.Collections.Generic;
using UnityEngine;
using DGW;
using System;
using System.Collections;
/*
* 
*/
[Serializable]
/// <summary> ����̓o�^���ɑΉ������l���i�[���邽�߂̋@�\��񋟂���N���X </summary>
public class PropertyDataBaseClass<TDataKey, TDataValue>
{
    //List<DataPair<������v���p�e�B�o�^���^,object�^�̃v���p�e�B�l�ێ��p�̌^>>
    //�Ď��Ώۂ̃v���p�e�B�̓o�^���Ɩ��O�̃f�[�^�x�[�X
    DataPair<TDataKey, TDataValue>[] _coreDataBase, _tempDataBase;
    public event Action OnDataAdded;
    public event Action OnDataRemoved;
    public PropertyDataBaseClass()//�R���X�g���N�^
    {
        _coreDataBase = new DataPair<TDataKey, TDataValue>[0];//�f�[�^�x�[�X�̃C���X�^���X��
        _tempDataBase = _coreDataBase;
    }
    ~PropertyDataBaseClass()//�f�R���X�g���N�^
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
/// Dictionary�݂����ȃf�[�^�y�A�̃N���X�B
/// List�Ƃ̕��p��z�肵�Đ݌v���Ă���B
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
    /// <summary> ������^��Key�̒l </summary>
    public string SKey => _key.ToString();
    /// <summary> ������^��Value�̒l </summary>
    public string SValue => _value.ToString();
    public DataPair(TKey key, TValue value)
    {
        _key = key;
        _value = value;
    }
    /// <summary> Key�̌^��Ԃ� </summary>
    public Type KeyType => typeof(TKey);
    /// <summary> Value�̌^��Ԃ� </summary>
    public Type ValueType => typeof(TValue);
}