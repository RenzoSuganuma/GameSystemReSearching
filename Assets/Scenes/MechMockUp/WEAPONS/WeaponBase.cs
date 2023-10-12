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
    //データ
    [SerializeField] WeaponStatusDataContainer _weaponData;
    //武器積載部位
    [SerializeField] WeaponStackPosition _wPosition;
    public WeaponStackPosition WeaponStackPosition => _wPosition;
    ACInputHandler _input;
    int _magazineAmounts;//マガジン数
    int _magazineSize;//マガジンサイズ
    int _heatLimit;//熱量限界値
    int _heatSpeed;//熱量加算値
    public int HeatSpd => _heatSpeed;
    int _firingRate;//発射レート[回/秒]
    int _firingAmounts;//発射弾数
    int _reloadingTime;//リロード時間
    int _coolingTime;//冷却時間
    //残弾数
    int _currentBullets;
    //熱量
    int _currentHeats;
    //リロードイベント
    public event Action OnReloadEnd = () => { Debug.Log("リロード完了！！！！！"); };
    //フラグ
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
        //データ抽出
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
    IEnumerator ReloadSequence(uint t)//一度だけ呼び出す
    {
        _isReloading = true;
        yield return new WaitForSeconds(t);
        Reload();
        _isReloading = false;
        _isFiringLock = false;
        OnReloadEnd();
    }
    /// <summary>強制冷却処理</summary>
    IEnumerator ForceCollingWeapon(uint t)
    {
        if (_isOverHeating)
        {
            Debug.Log("強制冷却");
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
            DoF(_isOverHeating, () =>//強制冷却処理
            {
                StartCoroutine(ForceCollingWeapon((uint)_coolingTime));
            });
        }
        else if (_currentBullets == 0)
        {
            Debug.Log("リロードしろ！！！！！");
            _isFiringLock = true;
        }
        Debug.Log("武器発射！！！！！");
    }
    void FiringSequence(uint rate)//bool が 真の時に呼び出される
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
    /// <summary>各処理を呼び出す</summary>
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