using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class WeaponBase : MonoBehaviour
{
    [SerializeField] WeaponStatusDataContainer _weaponData;
    protected event Action TaskOnStart = () => { Debug.Log("Weapon Start"); };
    int _magazineAmounts;//ƒ}ƒKƒWƒ“”
    int _magazineSize;//ƒ}ƒKƒWƒ“ƒTƒCƒY
    int _heatLimit;//”M—ÊŒÀŠE’l
    int _heatSpeed;//”M—Ê‰ÁŽZ’l
    int _firingRate;//”­ŽËƒŒ[ƒg[‰ñ/•b]
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