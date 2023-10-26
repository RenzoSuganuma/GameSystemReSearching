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
    [SerializeField, Range(1f, 10f)] float _fallSpeed;
    [SerializeField] List<GameObject> _targetsList = new();
    [SerializeField] LayerMask _targetObjectLayer;
    [SerializeField] LayerMask _groundLayer;
    [SerializeField] int _targetsLayerNumber;
    [SerializeField] int _currentTargetIndex;
    bool _isGrounded = false;
    bool _isPlayerApproaching = false;
    void AddGravityToThis() => _rb.AddForce((!_isGrounded) ? -transform.up * _fallSpeed * 100 : Vector3.zero);
    /// <summary> �ڒn���o </summary>
    void CheckGrounded()
    {
        _isGrounded = Physics.Raycast(transform.position, -transform.up, 1f, _groundLayer);
    }
    /// <summary> ���ׂĂ̓G�̌��� </summary>
    void SearchTargets()
    {
        var list = GameObject.FindObjectsOfType<GameObject>().Where(x => x.layer == _targetsLayerNumber).ToList();
        if (list.Count == 0) return;
        _targetsList = list;
    }
    /// <summary> �C�ӂ̓G�����m�����ɂ��邩���� </summary>
    /// <param name="start"> �n�_ </param>
    /// <param name="end"> �I�_ </param>
    /// <param name="limitDistance"> ���m���a </param>
    /// <param name="limitOffset"> ���m�����덷���e�l </param>
    /// <returns></returns>
    bool CheckTargetApproach(Vector3 start, Vector3 end, float limitDistance, float limitOffset)// �����x�[�X�v���C�����m
    {
        float dx = end.x - start.x;
        float dy = end.y - start.y;
        float dz = end.z - start.z;
        float dd = dx * dx + dy * dy + dz * dz;
        float lim = limitDistance * limitDistance;
        return dd < lim + limitOffset;
    }
    void ChaseWithTarget(bool isTargetInSight, GameObject target)
    {
        if (isTargetInSight)
        {
            var dir = (target.transform.position - transform.position).normalized;
            dir.y = 0;
            var v = dir * _moveSpeed;
            _rb.velocity = v;
        }
    }
    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.freezeRotation = true;
        SearchTargets();
    }
    void Update()
    {

    }
    void FixedUpdate()
    {
        CheckGrounded();
        AddGravityToThis();

        _isPlayerApproaching = CheckTargetApproach(transform.position, _targetsList[_currentTargetIndex].transform.position, _targetDetectRangeRadius, _targetDetectBufferRangeRadius);

        ChaseWithTarget(_isPlayerApproaching, _targetsList[_currentTargetIndex]);
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