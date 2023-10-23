using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
/// <summary> �ėp�Q�[���pAI�̋@�\��񋟂���N���X </summary>
/// �݌v�v�z�F���ɂƂ�r�w�C�r�A���L���[�ɒǉ����A��������s���I������Ȃ�A�L���[����폜����

// �L���[�ɃZ���h����r�w�C�r�A�͂�����x��܂��Ă��邩�A����̃r�w�C�r�A�ɂ͂��̃r�w�C�r�A��...�ƃA�^�b�`�ł���悤��
// UnityEvent�����蓖�ĂĂ��̂��P�̃A�v���[�`���낤�B
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