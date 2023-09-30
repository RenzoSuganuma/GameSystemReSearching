using System.Collections.Generic;
using UnityEngine;
using DebugLogRecorder;
using UnityEngine.Rendering;
/// <summary>AC�̃J��������R���|�[�l���g</summary>
public class ACCAMComponent : MonoBehaviour
{
    /// <summary>���̓n���h���[</summary>
    ACInputHandler _input;
    /// <summary>�I�N���[�W���������I�u�W�F�N�g���i�[���Ă���</summary>
    GameObject _occuludedObject;
    /// <summary>�J�����ߑ����̃Q�[���I�u�W�F�N�g</summary>
    List<GameObject> _visibleTargets = new();
    /// <summary>�������C�����O</summary>
    RuntimeLogComponent _log;
    /// <summary>�v���C���[</summary>
    ACMovementComponent _acMove;
    /// <summary>���ʂ̕����̃x�N�g��</summary>
    Vector3 _direction;
    /// <summary>���ʂ̕����̃x�N�g��(readonly)</summary>
    public Vector3 Forward => _direction;
    /// <summary>�J�����̒��S���W</summary>
    [SerializeField] Transform _centerTransform;
    /// <summary>�J�����ʒu�̃I�t�Z�b�g</summary>
    [SerializeField] Vector3 _offset = new(0, 15, 0);
    /// <summary>���͊��x</summary>
    [SerializeField] Vector2 _sencitivity = new(1, .5f);
    /// <summary>��]���a</summary>
    [SerializeField] float _rotateRadius;
    /// <summary>X����]�p�x�̃N�����v����Ƃ��̒l�̐�Βl</summary>
    [SerializeField, Range(.1f, .5f)] float _rollAngleAbsValue = .3f;
    /// <summary>��]�̔��]��L���ɂ��邩�̃t���O</summary>
    [SerializeField] bool _inverseRotation;
    /// <summary>�I�N���[�W����������̂ɃA�T�C�����铧���̕`�ʂ����邽�߂̃}�e���A��</summary>
    [SerializeField] Material _transparentMat;
    /// <summary>�J�����ړ��ɕK�v�ȎO�p�֐��̃V�[�^�ɑΉ�����lX��</summary>
    float _thetaX = 0;
    /// <summary>�J�����ړ��ɕK�v�ȎO�p�֐��̃V�[�^�ɑΉ�����lY��</summary>
    float _thetaY = 0;
    /// <summary>�Ə��A�V�X�g���邩�̃t���O</summary>
    bool _isTargetAssisting = false;
    private void Awake()
    {
        _input = GameObject.FindFirstObjectByType<ACInputHandler>();
    }
    private void OnEnable()
    {
        _input.LockOnAssist += StartTargetAssist;
    }
    private void OnDisable()
    {
        _input.LockOnAssist -= StartTargetAssist;
    }
    void Start()
    {
        //NULL��������x�����O��f���o��
        if (GetComponent<Camera>() == null) Debug.LogWarning("�v���C���[�J������������Ȃ�");
        if (_centerTransform == null) Debug.LogWarning("�^�[�Q�b�g�̍��W��null����");
        this.gameObject.tag = "MainCamera";
        _acMove = GameObject.FindFirstObjectByType<ACMovementComponent>();
        _log = new(new Rect(0, 500, 300, 300));
        TargettingSequence(_centerTransform);
    }
    void Update()
    {
        RotateSequence();
        TargettingSequence(_centerTransform);
        TargettignAssistSequence(_isTargetAssisting);
        OcculusionSequence();
    }
    #region private���\�b�h
    /// <summary>�I�N���[�W��������</summary>
    private void OcculusionSequence()
    {
        //�^�[�Q�b�g�Ƃ̋����̎Z�o
        var dis = Vector3.Distance(_centerTransform.position, this.transform.position);
        //�^�[�Q�b�g�Ɍ����������̃x�N�g���̎Z�o
        var dir = _centerTransform.position - this.transform.position;
        //�����̐���
        Ray ray = new(this.transform.position, dir);
        RaycastHit hit;
        Debug.DrawRay(ray.origin, ray.direction, Color.magenta, dis);
        //�����������ɓ���������
        if (Physics.Raycast(ray, out hit))
        {
            //�I�N���[�W��������
            if (hit.transform.gameObject.TryGetComponent<OcculutionTarget>(out OcculutionTarget target))
            {
                target.OverwriteMaterial(_transparentMat);
                _occuludedObject = target.gameObject;
            }
            //�I�N���[�W������������
            else if (_occuludedObject != null)
            {
                if (_occuludedObject.TryGetComponent<OcculutionTarget>(out OcculutionTarget component))
                {
                    component.OverwriteMaterial(component.Material);
                }
            }
            //Debug.Log($"{nameof(OcculusionSequence)}:{hit.transform.gameObject.name}");
        }
    }
    /// <summary>Y����]����</summary>
    private void RotateSequence()
    {
        //���͏���
        float inputX = _input.LookInput.x * _sencitivity.x * .01f;
        _thetaX += inputX;
        float inputY = _input.LookInput.y * _sencitivity.y * .01f;
        _thetaY += inputY;
        //X����]�Ɏg�������̒l�̃N�����v
        if (_acMove.IsGrounded)//�ڒn��
        {
            _thetaY = Mathf.Clamp(_thetaY, -_rollAngleAbsValue, _rollAngleAbsValue);
        }
        else if (_acMove.IsHovering)//�؋�
        {
            _thetaY = Mathf.Clamp(_thetaY, -_rollAngleAbsValue * 2, _rollAngleAbsValue * 2);
        }
        //��]�̔��]�̕����̏�����
        var sign = (_inverseRotation) ? -1 : 1;
        //���W�X�V
        this.transform.position =
            new Vector3(Mathf.Cos(_thetaX) * sign, Mathf.Sin(_thetaY) * sign, Mathf.Sin(_thetaX) * sign)
            * _rotateRadius + _centerTransform.position + _offset;
    }
    /// <summary>�ߑ�����</summary>
    private void TargettingSequence(Transform targetTransform)
    {
        //LookRotation�̑������ɐ��ʕ����̃x�N�g�����w�肵�ă^�[�Q�b�g�̃I�u�W�F�N�g������
        this.transform.rotation =
            Quaternion.LookRotation(targetTransform.position - this.transform.position
            , Vector3.up);
        //���ʃx�N�g���̏�����
        _direction = new(this.transform.forward.x, 0, this.transform.forward.z);
    }
    private void StartTargetAssist()
    {
        _isTargetAssisting = !_isTargetAssisting;
    }
    private void TargettignAssistSequence(bool isActive)
    {

    }
    #endregion
    #region public���\�b�h
    /// <summary>�J�����ߑ����̃I�u�W�F�N�g��o�^����</summary>
    /// <param name="target"></param>
    public void AddVisibleObjectInList(GameObject target)
    {
        _visibleTargets.Add(target);
    }
    /// <summary>�J�����ߑ����̃I�u�W�F�N�g��o�^��������</summary>
    public void RemoveVisibleObjectInList(GameObject target)
    {
        _visibleTargets.Remove(target);
    }
    #endregion
    private void OnDrawGizmos()
    {
        //��]���a�̋����b�V���`��
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(_centerTransform.position, _rotateRadius);
    }
    private void OnGUI()
    {
        //_log.DisplayLog($"{_visibleTargets[0].name}");
    }
}