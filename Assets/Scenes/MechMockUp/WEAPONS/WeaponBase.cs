using static DGW.OriginalMethods;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum WeaponSequence
{
    FiringSequence,
    ReloadSequence,
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
    ACInputHandler _input;
    int _magazineAmounts;//�}�K�W����
    int _magazineSize;//�}�K�W���T�C�Y
    int _heatLimit;//�M�ʌ��E�l
    int _heatSpeed;//�M�ʉ��Z�l
    public int HeatSpd => _heatSpeed;
    int _firingRate;//���˃��[�g[��/�b]
    int _firingAmounts;//���˒e��
    int _reloadingTime;//�����[�h����
    int _coolingTime;//��p����
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
    bool _isFiringLock = false;
    public bool IsFireLocked => _isFiringLock;
    //Temporary Properties
    float _countedTime = 0;
    private void Start()
    {
        _input = GameObject.FindAnyObjectByType<ACInputHandler>();
        //�f�[�^���o
        this._magazineAmounts = _weaponData._magazineAmounts;
        this._magazineSize = _weaponData._magazineSize;
        this._heatLimit = _weaponData._heatLimit;
        this._heatSpeed = _weaponData._heatSpeed;
        this._firingRate = _weaponData._firingRate;
        this._firingAmounts = _weaponData._firingAmounts;
        this._reloadingTime = _weaponData._reloadingTime;
        this._coolingTime = _weaponData._coolingTime;
        Reload();
        _currentHeats = 0;
    }
    private void Update()
    {
        if (_input.IsLfire && !_isFiringLock)
            CallBehaviour(WeaponSequence.FiringSequence);
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
        _isFiringLock = false;
        OnReloadEnd();
    }
    /// <summary>������p����</summary>
    IEnumerator ForceCollingWeapon(uint t)
    {
        if (_isOverHeating)
        {
            Debug.Log("������p");
            //_forcedCooling = true;
            if (!_isFiringLock) _isFiringLock = true;
            yield return new WaitForSeconds(t);
            _currentHeats = 0;
            _isOverHeating = false;
            if (_isFiringLock) _isFiringLock = false;
        }
    }
    void Fire(int decreseValue)
    {
        if (_currentBullets - decreseValue >= 0 && !_isOverHeating)
        {
            _currentBullets -= decreseValue;
            _currentHeats += _heatSpeed;
            _isOverHeating = (_currentHeats > _heatLimit) ? true : false;
            DoF(_isOverHeating, () =>//������p����
            {
                StartCoroutine(ForceCollingWeapon((uint)_coolingTime));
            });
        }
        else if (_currentBullets == 0)
        {
            Debug.Log("�����[�h����I�I�I�I�I");
            _isFiringLock = true;
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
    /// <summary>�e�������Ăяo��</summary>
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
        }
    }
    /* --------------------------------------------------------------- */
}