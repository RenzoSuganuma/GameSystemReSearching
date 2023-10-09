using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Weapon_Test : WeaponBase
{
    int _currentBullets;
    private void Start()
    {
        _currentBullets = base.MagCnt * base.MagSize;
    }
    void Fire(int decreseValue)
    {
        if (_currentBullets - decreseValue > 0)
        {
            _currentBullets -= decreseValue;
        }
    }
}