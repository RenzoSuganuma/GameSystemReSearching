using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DiscoveryGameWorks.OriginalMethods;
public class Weapon_Test : WeaponBase
{
    [SerializeField] Transform _muzzlePosition;
    [SerializeField] GameObject _bullet;
    protected override void OnFired()
    {
        var bullet = Instantiate(_bullet);
        bullet.transform.position = _muzzlePosition.position;
    }
    protected override void OnReloaded()
    {
        
    }
}