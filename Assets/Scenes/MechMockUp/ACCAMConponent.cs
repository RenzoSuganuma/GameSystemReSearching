using System.Collections.Generic;
using UnityEngine;
using DebugLogRecorder;
using System;
/// <summary>AC�̃J��������R���|�[�l���g</summary>
public class ACCAMComponent : MonoBehaviour
{
    /// <summary>���̓n���h���[</summary>
    ACInputHandler _input;
    /// <summary>�I�N���[�W���������I�u�W�F�N�g���i�[���Ă���</summary>
    GameObject _occuludedObject;
    /// <summary>�J�����̃I�u�W�F�N�g</summary>
    GameObject _parentObject;
    /// <summary>���b�N�I���\�ȃI�u�W�F�N�g���i�[���Ă���</summary>
    List<Transform> _lockOnTargetTransform = new();
    /// <summary>���b�N�I���\�ȃI�u�W�F�N�g���i�[���Ă���</summary>
    public List<Transform> LockOnTargetList => _lockOnTargetTransform;
    /// <summary>���b�N�I�������������ꂽ�Ƃ��ɌĂяo�����</summary>
    public event Action LockOnDeniedEvent = () => { Debug.Log("LockOnDenied"); };
    /// <summary>�������C�����O</summary>
    RuntimeLogComponent _log;
    /// <summary>�v���C���[</summary>
    ACMovementComponent _acMove;
    /// <summary>���ʂ̕����̃x�N�g��</summary>
    Vector3 _direction;
    /// <summary>�O�t���[���̐��ʂ̕����̃x�N�g��</summary>
    Vector3 _pDirection;
    /// <summary>�O�t���[���̐��ʂ̕����̃x�N�g��</summary>
    public Vector3 PastFrameDirection => _pDirection;
    /// <summary>���ʂ̕����̃x�N�g��(readonly)</summary>
    public Vector3 Forward => _direction;
    /// <summary>�J�����̒��S���W</summary>
    [SerializeField] Transform _centerTransform;
    /// <summary>�J�����I�t�Z�b�g</summary>
    [SerializeField] Vector3 _offset;
    /// <summary>�J�����I�t�Z�b�g</summary>
    [SerializeField] Vector3 _lookOffset;
    /// <summary>���͊��x</summary>
    [SerializeField] Vector2 _sencitivity = new(1, .5f);
    /// <summary>��]���a</summary>
    [SerializeField] float _rotateRadius;
    /// <summary>��]���a</summary>
    [SerializeField] float _targettingLimitDistance;
    /// <summary>��]���a</summary>
    public float TargettingLimitDistance => _targettingLimitDistance;
    /// <summary>X����]�p�x�̃N�����v����Ƃ��̒l�̐�Βl</summary>
    [SerializeField, Range(.1f, .5f)] float _rollAngleAbsValue = .3f;
    /// <summary>��]�̔��]��L���ɂ��邩�̃t���O</summary>
    [SerializeField] bool _inverseRotationY;
    /// <summary>��]�̔��]��L���ɂ��邩�̃t���O</summary>
    [SerializeField] bool _inverseRotationX;
    /// <summary>�I�N���[�W����������̂ɃA�T�C�����铧���̕`�ʂ����邽�߂̃}�e���A��</summary>
    [SerializeField] Material _transparentMat;
    /// <summary>�J�����ړ��ɕK�v�ȎO�p�֐��̃V�[�^�ɑΉ�����lX��</summary>
    float _thetaX = 0;
    /// <summary>�J�����ړ��ɕK�v�ȎO�p�֐��̃V�[�^�ɑΉ�����lY��</summary>
    float _thetaY = 0;
    /// <summary>�Ə��A�V�X�g���邩�̃t���O</summary>
    bool _isTargetAssisting = false;
    /// <summary>�Ə��A�V�X�g���邩�̃t���O</summary>
    public bool IsTargetAssisting => _isTargetAssisting;
    /// <summary>�O�t���[���̏Ə��A�V�X�g�̃t���O</summary>
    bool _pLockOnState = false;
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
        _parentObject = new GameObject("CameraPositionReference");
        this.transform.parent = _parentObject.transform;
        this.transform.localPosition = Vector3.zero;
        ACCAMLookSequence(_centerTransform);
    }
    void Update()
    {
        RotateSequence(_isTargetAssisting, (_isTargetAssisting) ? _rotateRadius / 2 : _rotateRadius);
        ACCAMLookSequence(_centerTransform);
        OcculusionSequence();
        var lockOnPermitState = (_lockOnTargetTransform != null && _lockOnTargetTransform.Count > 0)
            ? _lockOnTargetTransform[0].GetComponent<LockOnTarget>().IsCanLockOn : false;
        LockOnSequence(_isTargetAssisting, lockOnPermitState);
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
        }
    }
    /// <summary>Y����]����</summary>
    private void RotateSequence(bool isTargetAssisting, float rotateRadius)
    {
        if (!isTargetAssisting)
        {
            float inputX = _input.LookInput.x * _sencitivity.x * .01f;
            _thetaX += inputX;
            float inputY = _input.LookInput.y * _sencitivity.y * .01f;
            _thetaY += inputY;
        }//X����]�Ɏg�������̒l�̃N�����v
        if (_acMove.IsGrounded)//�ڒn��
        {
            _thetaY = Mathf.Clamp(_thetaY, -_rollAngleAbsValue, _rollAngleAbsValue);
        }
        else if (_acMove.IsHovering)//�؋�
        {
            _thetaY = Mathf.Clamp(_thetaY, -_rollAngleAbsValue * 2, _rollAngleAbsValue * 2);
        }
        //��]�̔��]�̕����̏�����
        var signX = (_inverseRotationX) ? -1 : 1;
        var signY = (_inverseRotationY) ? -1 : 1;
        if (!_isTargetAssisting)
        {
            _parentObject.transform.position =
                new Vector3(Mathf.Cos(_thetaX) * signX
                , Mathf.Sin(_thetaY) * signY
                , Mathf.Sin(_thetaX) * signX)
                * rotateRadius
                + _centerTransform.position;
            this.transform.localPosition =
                  (transform.forward * _offset.z)
                + (transform.right * _offset.x)
                + (transform.up * _offset.y);
        }
        else
        {
            _parentObject.transform.position = _centerTransform.position + (_centerTransform.forward * -_rotateRadius) + (_centerTransform.up * 5);
        }//���W�X�V
    }
    /// <summary>�^�[�Q�b�g�A�V�X�g���͂��������Ƃ��ɌĂяo�����</summary>
    private void StartTargetAssist()
    {
        if (_lockOnTargetTransform != null && _lockOnTargetTransform.Count > 0)
        {
            _isTargetAssisting = !_isTargetAssisting;
        }
    }
    /// <summary>�v���C���[�ߑ�����</summary>
    private void ACCAMLookSequence(Transform followTransform)
    {
        //LookRotation�̑������ɐ��ʕ����̃x�N�g�����w�肵�ă^�[�Q�b�g�̃I�u�W�F�N�g������
        this.transform.rotation =
        Quaternion.LookRotation((followTransform.position - this.transform.position) + _lookOffset + _offset
        , Vector3.up);
        //���ʃx�N�g���̏�����
        _direction = new(this.transform.forward.x, 0, this.transform.forward.z);
    }
    /// <summary>���b�N�I������</summary>
    /// <param name="isAssistingAim"></param>
    /// <param name="isCanLockOn"></param>
    private void LockOnSequence(bool isAssistingAim, bool isCanLockOn)
    {
        if (isAssistingAim && _lockOnTargetTransform != null && _lockOnTargetTransform.Count > 0 && isCanLockOn)
        {
            _pLockOnState = isCanLockOn;
            //LookRotation�̑������ɐ��ʕ����̃x�N�g�����w�肵�ă^�[�Q�b�g�̃I�u�W�F�N�g������
            this.transform.rotation =
                Quaternion.LookRotation((_lockOnTargetTransform[0].position - this.transform.position) + _lookOffset + _offset
                , Vector3.up);
            //���ʃx�N�g���̏�����
            _direction = new(this.transform.forward.x, 0, this.transform.forward.z);
            //�A�V�X�g��������
            if (Mathf.Abs(_input.LookInput.x) > .75f || Mathf.Abs(_input.LookInput.y) > .75f)
            {
                _isTargetAssisting = false;
            }
        }
        else if (_pLockOnState)
        {
            _pDirection = (_lockOnTargetTransform[0].transform.position - _parentObject.transform.position);
            _pDirection.y = 0;
            _isTargetAssisting = false;
            _pLockOnState = false;
            LockOnDeniedEvent();//�������b�N�I�������C�x���g
        }
    }
    #endregion
    #region public���\�b�h
    public void AddLockOnTargetToList(Transform target)
    {
        _lockOnTargetTransform.Add(target);
    }
    public void RemoveLockOnTargetToList(Transform target)
    {
        _lockOnTargetTransform.Remove(target);
    }
    #endregion
    private void OnDrawGizmos()
    {
        //��]���a�̋����b�V���`��
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(_centerTransform.position, _rotateRadius);
    }
}