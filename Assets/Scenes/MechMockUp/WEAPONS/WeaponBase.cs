using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class WeaponBase : MonoBehaviour
{
    [SerializeField] WeaponStatusDataContainer _weaponData;
    protected event Action TaskOnStart = () => { Debug.Log("Weapon Start"); };
    int _magazineAmounts;//�}�K�W����
    int _magazineSize;//�}�K�W���T�C�Y
    int _heatLimit;//�M�ʌ��E�l
    int _heatSpeed;//�M�ʉ��Z�l
    int _firingRate;//���˃��[�g[��/�b]
    protected int MagCnt => _magazineAmounts;
    protected int MagSize => _magazineSize;
    protected int HeatLim => _heatLimit;
    protected int HeatSpd => _heatSpeed;
    protected int FireRate => _firingRate;
    private void Start()
    {
        this._magazineAmounts = _weaponData._magazineAmounts;
        this._magazineSize = _weaponData._magazineSize;
        this._heatLimit = _weaponData._heatLimit;
        this._heatSpeed = _weaponData._heatSpeed;
        this._firingRate = _weaponData._firingRate;
        TaskOnStart();
    }
}