using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DGW.OriginalMethods;
public class Weapon_Test : WeaponBase
{
    ACInputHandler _input;
    private void Awake()
    {
        _input = GameObject.FindAnyObjectByType<ACInputHandler>();
    }
    private void OnEnable()
    {
    }
    private void OnDisable()
    {
    }
    private void Update()
    {
        FiringNow();
        CollingNow();
        DoF(base.IsOverHeat, () =>
        {
            Debug.Log("オーバーヒーティング");
        });
    }

    protected override void FiringNow()
    {
        if (_input.IsLfire && !base.IsCannotFire)
            base.CallBehaviour(WeaponSequence.FiringSequence);
    }

    protected override void CollingNow()
    {
        if (base.IsOverHeat)
            base.CallBehaviour(WeaponSequence.CoolingSequence);
    }
}