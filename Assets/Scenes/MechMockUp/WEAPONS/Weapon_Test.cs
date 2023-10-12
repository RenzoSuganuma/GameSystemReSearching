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
        FiringCheck();
        CollingCheck();
        DoF(base.IsOverHeat, () =>
        {
            Debug.Log("オーバーヒーティング");
        });
    }
    protected override void FiringCheck()
    {
        if (_input.IsLfire && !base.IsFireLocked)
            base.CallBehaviour(WeaponSequence.FiringSequence);
    }
    protected override void CollingCheck()
    {
        if (base.IsOverHeat)
            base.CallBehaviour(WeaponSequence.CoolingSequence);
    }
}