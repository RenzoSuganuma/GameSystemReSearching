using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
/// <summary> 汎用ゲーム用AIの機能を提供するクラス </summary>
/// 設計思想：次にとるビヘイビアをキューに追加し、それを実行し終わったなら、キューから削除する

// キューにセンドするビヘイビアはある程度定まっているか、特定のビヘイビアにはこのビヘイビアを...とアタッチできるように
// UnityEventを割り当ててやるのも１つのアプローチだろう。
public class AI_Dev_alpha : MonoBehaviour
{
    // Idle
    protected Action OnIdle;
    // Patroll
    protected Action OnPatroll;
    // Chase
    // Attack
    private void Start()
    {
        
    }
    private void FixedUpdate()
    {
        
    }
}