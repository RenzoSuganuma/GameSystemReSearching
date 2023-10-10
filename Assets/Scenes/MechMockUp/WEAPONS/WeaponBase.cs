using DGW;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum WeaponSequence
{
    FiringSequence,
    ReloadSequence,
    CoolingSequence,
}
public enum WeaponStackPosition
{
    RArm,
    LArm,
    RSHoulder,
    LShoulder,
}
public abstract class WeaponBase : MonoBehaviour
{
    //�f�[�^
    [SerializeField] WeaponStatusDataContainer _weaponData;
    //����ύڕ���
    [SerializeField] WeaponStackPosition _wPosition;
    public WeaponStackPosition WeaponStackPosition => _wPosition;
    int _magazineAmounts;//�}�K�W����
    int _magazineSize;//�}�K�W���T�C�Y
    int _heatLimit;//�M�ʌ��E�l
    int _heatSpeed;//�M�ʉ��Z�l
    public int HeatSpd => _heatSpeed;
    int _firingRate;//���˃��[�g[��/�b]
    int _firingAmounts;//���˒e��
    int _reloadingTime;//�����[�h����
    //�c�e��
    int _currentBullets;
    //�M��
    int _currentHeats;
    //�����[�h�C�x���g
    public event Action OnReloadEnd = () => { Debug.Log("�����[�h�����I�I�I�I�I"); };
    //�t���O
    bool _isReloading = false;
    public bool IsReloading => _isReloading;
    bool _isOverHeating = false;
    public bool IsOverHeat => _isOverHeating;
    bool _cannotFire = false;
    public bool IsCannotFire => _cannotFire;
    //Temporary Properties
    float _countedTime = 0;
    private void Start()
    {
        //�f�[�^���o
        this._magazineAmounts = _weaponData._magazineAmounts;
        this._magazineSize = _weaponData._magazineSize;
        this._heatLimit = _weaponData._heatLimit;
        this._heatSpeed = _weaponData._heatSpeed;
        this._firingRate = _weaponData._firingRate;
        this._firingAmounts = _weaponData._firingAmounts;
        this._reloadingTime = _weaponData._reloadingTime;
        Reload();
        _currentHeats = 0;
    }
    void Reload()
    {
        _currentBullets = _magazineSize;
        _magazineAmounts--;
    }
    IEnumerator ReloadSequence(uint t)//��x�����Ăяo��
    {
        _isReloading = true;
        yield return new WaitForSeconds(t);
        Reload();
        _isReloading = false;
        _cannotFire = false;
        OnReloadEnd();
    }
    void Fire(int decreseValue)
    {
        if (_currentBullets - decreseValue >= 0 && !_isOverHeating)
        {
            _currentBullets -= decreseValue;
            _currentHeats += _heatSpeed;
            _isOverHeating = (_currentHeats > _heatLimit) ? true : false;
        }
        else if (_currentBullets == 0)
        {
            Debug.Log("�����[�h����I�I�I�I�I");
            _cannotFire = true;
        }
        Debug.Log("���픭�ˁI�I�I�I�I");
    }
    void FiringSequence(uint rate)//bool �� �^�̎��ɌĂяo�����
    {
        var sec = 1.0f / rate;
        _countedTime += Time.deltaTime;
        if (_countedTime >= sec)
        {
            Fire(_firingAmounts);
            _countedTime = 0;
        }
    }
    /* ------------------------------------ */
    /// <summary>���C�܂��̓����[�h�������Ăяo��</summary>
    /// <param name="seq"></param>
    public void CallBehaviour(WeaponSequence seq)
    {
        switch (seq)
        {
            case WeaponSequence.FiringSequence:
                {
                    FiringSequence((uint)_firingRate);
                    break;
                }
            case WeaponSequence.ReloadSequence:
                {
                    StartCoroutine(ReloadSequence((uint)_reloadingTime));
                    break;
                }
            case WeaponSequence.CoolingSequence:
                {
                    StartCoroutine(ForceCollingWeapon(3));
                    break;
                }
        }
    }
    /// <summary>������p����</summary>
    IEnumerator ForceCollingWeapon(uint t)
    {
        yield return new WaitForSeconds(t);
        _currentHeats = 0;
        _isOverHeating = false;
    }
    /* --------------------------------------------------------------- */
    /// <summary>���˓��͂����鎞�Ɍp���I�ɌĂяo�����</summary>
    protected abstract void FiringNow();
    /// <summary>���˓��͂��Ȃ��Ƃ��Ɍp���I�ɌĂяo�����</summary>
    protected abstract void CollingNow();
}