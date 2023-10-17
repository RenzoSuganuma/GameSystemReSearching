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
public class DataDictionary<TDataKey, TDataValue>
{
    //List<DataPair<������v���p�e�B�o�^���^,object�^�̃v���p�e�B�l�ێ��p�̌^>>
    //�Ď��Ώۂ̃v���p�e�B�̓o�^���Ɩ��O�̃f�[�^�x�[�X
    DataPair<TDataKey, TDataValue>[] _coreDataBase;
    public event Action OnDataAdded;
    public event Action OnDataRemoved;
    public DataDictionary()//�R���X�g���N�^
    {
        _coreDataBase = new DataPair<TDataKey, TDataValue>[0];//�f�[�^�x�[�X�̃C���X�^���X��
    }
    ~DataDictionary()//�f�R���X�g���N�^
    {
        _coreDataBase = null;
        OnDataAdded = null;
        OnDataRemoved = null;
    }
    /// <summary> �f�[�^�̓o�^ </summary>
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
    /// <summary> �f�[�^�̓o�^���� </summary>
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