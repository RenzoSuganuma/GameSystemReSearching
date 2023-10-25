using System.Collections.Generic;
using System.Linq;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class GeneralAICore : MonoBehaviour
{
    Rigidbody _rb;
    [SerializeField, Range(1f, 50f)] float _targetDetectRangeRadius;
    [SerializeField, Range(0, 5)] float _targetDetectBufferRangeRadius;
    [SerializeField, Range(1f, 50f)] float _attackingRangeRadius;
    [SerializeField, Range(1f, 100f)] float _moveSpeed;
    [SerializeField, Range(1f, 100f)] float _fallSpeed;
    [SerializeField] List<GameObject> _targetsList = new();
    [SerializeField] LayerMask _targetObjectLayer;
    [SerializeField] LayerMask _groundLayer;
    [SerializeField] int _targetsLayerNumber;
    [SerializeField] int _currentTargetIndex;
    bool _isGrounded = false;
    bool _isPlayerApproaching = false;
    /// <summary> 接地検出 </summary>
    void CheckGrounded() => _isGrounded = Physics.CheckSphere(transform.position, 1, _targetObjectLayer);
    /// <summary> すべての敵の検索 </summary>
    void SearchTargets()
    {
        var list = GameObject.FindObjectsOfType<GameObject>().Where(x => x.layer == _targetsLayerNumber).ToList();
        if (list.Count == 0) return;
        _targetsList = list;
    }
    /// <summary> 任意の敵が検知圏内にいるか判定 </summary>
    /// <param name="start"> 始点 </param>
    /// <param name="end"> 終点 </param>
    /// <param name="limitDistance"> 検知半径 </param>
    /// <param name="limitOffset"> 検知距離誤差許容値 </param>
    /// <returns></returns>
    bool CheckTargetApproach(Vector3 start, float limitDistance, float limitOffset)// 距離ベースプレイヤ検知
    {
        if (_targetsList.Count == 0) return false;
        var end = _targetsList[_currentTargetIndex].transform.position;
        float dx = end.x - start.x;
        float dy = end.y - start.y;
        float dz = end.z - start.z;
        float dd = dx * dx + dy * dy + dz * dz;
        float lim = limitDistance * limitDistance;
        return dd < lim + limitOffset;
    }
    void ChaseWithTarget(bool isTargetInSight)
    {
        if (_targetsList.Count == 0) return;
        if (isTargetInSight)
        {
            var dir = (_targetsList[_currentTargetIndex].transform.position - transform.position).normalized;
            dir.y = 0;
            var v = dir * _moveSpeed;
            _rb.velocity = v;
        }
    }
    void AddGravityToThis()
    {

    }
    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.freezeRotation = true;
        SearchTargets();
    }
    void Update()
    {
        AddGravityToThis();
    }
    void FixedUpdate()
    {
        CheckGrounded();

        _isPlayerApproaching = CheckTargetApproach(transform.position, _targetDetectRangeRadius, _targetDetectBufferRangeRadius);

        ChaseWithTarget(_isPlayerApproaching);
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _targetDetectRangeRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _attackingRangeRadius);
        Gizmos.color = Color.green;
    }
}