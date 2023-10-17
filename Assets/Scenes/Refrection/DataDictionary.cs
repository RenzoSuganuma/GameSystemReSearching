using System.Collections.Generic;
using UnityEngine;
using System;
using System.Collections;
using TMPro.EditorUtilities;
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
    public TDataValue this[TDataKey key]
    {
        get { return this.Find(key); }
        set { this.SetAt(key, value); }
    }
    public TDataKey this[TDataValue dataValue]
    {
        get { return this.Find(dataValue); }
        set { this.SetAt(dataValue, value); }
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
    /// <summary>  </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public TDataKey Find(TDataValue value)
    {
        for (int i = 0; i < _coreDataBase.Length; i++)
        {
            if (_coreDataBase[i].Value.Equals(value))
            {
                return _coreDataBase[i].Key;
            }
        }
        return default(TDataKey);
    }
    /// <summary>  </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public TDataValue Find(TDataKey key)
    {
        for (int i = 0; i < _coreDataBase.Length; i++)
        {
            if (_coreDataBase[i].Key.Equals(key))
            {
                return _coreDataBase[i].Value;
            }
        }
        return default(TDataValue);
    }
    public void SetAt(TDataValue value, TDataKey key)//Set Key By Value
    {
        for (int i = 0; i < _coreDataBase.Length; i++)
        {
            if (_coreDataBase[i].Value.Equals(value))
            {
                _coreDataBase[i].SetKey(key);
                break;
            }
        }
    }
    public void SetAt(TDataKey key, TDataValue value)//Set Value By Key
    {
        for (int i = 0; i < _coreDataBase.Length; i++)
        {
            if (_coreDataBase[i].Key.Equals(key))
            {
                _coreDataBase[i].SetValue(value);
                break;
            }
        }
    }
    /// <summary> �f�[�^�y�A��Ԃ� </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public DataPair<TDataKey, TDataValue> GetDataPair(int index)
    {
        return (_coreDataBase[index] != null) ? _coreDataBase[index] : null;
    }
    /// <summary> ����̃C���f�b�N�X�Ƀf�[�^�������� </summary>
    /// <param name="pair"></param>
    /// <param name="index"></param>
    public void SetDataPair(DataPair<TDataKey, TDataValue> pair, int index)
    {
        _coreDataBase[index] = pair;
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
    public DataPair(TKey key, TValue value)//�R���X�g���N�^
    {
        _key = key;
        _value = value;
    }
    /// <summary> Key���Z�b�g </summary>
    /// <param name="key"></param>
    public void SetKey(TKey key)
    {
        this._key = key;
    }
    /// <summary> Value���Z�b�g </summary>
    public void SetValue(TValue value)
    {
        this._value = value;
    }
    /// <summary> Key�̌^��Ԃ� </summary>
    public Type KeyType => typeof(TKey);
    /// <summary> Value�̌^��Ԃ� </summary>
    public Type ValueType => typeof(TValue);
}