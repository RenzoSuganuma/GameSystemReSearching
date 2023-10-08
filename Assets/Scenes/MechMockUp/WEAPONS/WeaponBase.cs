using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class WeaponBase : MonoBehaviour
{
    [SerializeField] WeaponStatusDataContainer _datas;
    protected event Action ProcessWhenStart = () => { Debug.Log("Weapon Start"); };
    int _bulletsCount;//���ׂĂ̎c�e
    int _magazineSize;//�}�K�W���T�C�Y
    int _heatLimit;//�M�ʌ��E�l
    int _heatSpeed;//�M�ʉ��Z�l
    int _firingRate;//���˃��[�g[��/�b]
    private void Start()
    {
        ProcessWhenStart();
        _bulletsCount = _datas._bulletsCount;
        _magazineSize = _datas._magazineSize;
        _heatLimit = _datas._heatLimit;
        _heatSpeed = _datas._heatSpeed;
        _firingRate = _datas._firingRate;
    }
    protected int GetAllBullets() { return _bulletsCount; }
    protected int GetMagSize() { return _magazineSize; }
    protected int GetThermalLimit() { return _heatLimit; }
    protected int GetHeatingSpeed() { return _heatSpeed; }
    protected int GetFiringRate() { return _firingRate; }
}