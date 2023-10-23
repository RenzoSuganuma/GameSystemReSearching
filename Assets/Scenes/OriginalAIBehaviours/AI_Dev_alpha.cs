using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
/// <summary> 汎用ゲーム用AIの機能を提供するクラス </summary>
/// 設計思想：次にとるビヘイビアをキューに追加し、それを実行し終わったなら、キューから削除する

// キューにセンドするビヘイビアはある程度定まっているか、特定のビヘイビアにはこのビヘイビアを...とアタッチできるように
// UnityEventを割り当ててやるのも１つのアプローチだろう。
[RequireComponent(typeof(Rigidbody))]
public class AI_Dev_alpha : MonoBehaviour
{
    // Idle
    protected Action OnIdle;
    // Patroll
    protected Action OnPatroll;
    // Chase
    protected Action OnChase;
    // Attack
    protected Action OnAttack;

    [SerializeField] float _moveSpeed;
    [SerializeField] int _targettingSightRangeRadius;
    [SerializeField] int _attackingRangeRadius;
    [SerializeField] int _targetLayerNumber;

    Rigidbody _rb;
    public List<GameObject> _targetObjects = new();
    public int _targetObjIndex = 0;
    public bool _isFound = false;
    public bool _isCanAttack = false;
    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _targetObjects = GameObject.FindObjectsOfType<GameObject>().Where(x => x.layer == _targetLayerNumber).ToList();
    }
    private void FixedUpdate()
    {
        
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _targettingSightRangeRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _attackingRangeRadius);
    }
}