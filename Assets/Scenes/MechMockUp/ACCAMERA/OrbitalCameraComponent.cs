using System.Collections.Generic;
using UnityEngine;
using System;
using RS;
/// <summary>AC�̃J��������R���|�[�l���g</summary>
public class OrbitalCameraComponent : MonoBehaviour
{
    ACInputHandler _input;
    MechMovementComponent _acMove;
    /// <summary>�I�N���[�W���������N���X</summary>
    Occulutioner _occ;
    /// <summary>���ʃx�N�g��</summary>
    Vector3 _forward;
    /// <summary>���ʃx�N�g��</summary>
    public Vector3 Forward => _forward;
    /// <summary>�E�x�N�g��</summary>
    Vector3 _right;
    /// <summary>�E�x�N�g��</summary>
    public Vector3 Right => _right;
    /// <summary>�J�����̒��S���W</summary>
    [SerializeField] Transform _centerTransform;
    /// <summary>���͊��x</summary>
    [SerializeField] Vector2 _sencitivity = new(1, .5f);
    /// <summary>��]���a</summary>
    [SerializeField] float _rotateRadius;
    /// <summary>X����]�p�x�̃N�����v����Ƃ��̒l�̐�Βl</summary>
    [SerializeField, Range(.1f, .5f)] float _rollAngleAbsValue = .3f;
    /// <summary>��]�̔��]��L���ɂ��邩�̃t���O</summary>
    [SerializeField] bool _inverseRotationY;
    /// <summary>��]�̔��]��L���ɂ��邩�̃t���O</summary>
    [SerializeField] bool _inverseRotationX;
    /// <summary>�J�����ړ��ɕK�v�ȎO�p�֐��̃V�[�^�ɑ�������lX��</summary>
    float _thetaX = 0;
    /// <summary>�J�����ړ��ɕK�v�ȎO�p�֐��̃V�[�^�ɑ�������lY��</summary>
    float _thetaY = 0;
    void Awake() => _input = GameObject.FindFirstObjectByType<ACInputHandler>();
    void Start()
    {
        //NULL��������x�����O��f���o��
        if (GetComponent<Camera>() == null) Debug.LogWarning("�v���C���[�J������������Ȃ�");
        if (_centerTransform == null) Debug.LogWarning("�^�[�Q�b�g�̍��W��null����");
        //Update Name
        this.gameObject.tag = "MainCamera";
        _acMove = GameObject.FindAnyObjectByType<MechMovementComponent>();
        _occ = GetComponent<Occulutioner>();
    }
    void FixedUpdate()
    {
        GetLookInput();
        RotateSequence(_rotateRadius);
        TargettingSequence(_centerTransform);
        OcculusionSequence();
    }
    #region private���\�b�h
    /// <summary>���_�ړ����͎󂯎��A�i�[����</summary>
    void GetLookInput()
    {
        float inputX = _input.LookInput.x * _sencitivity.x * .01f;
        _thetaX += inputX;
        float inputY = _input.LookInput.y * _sencitivity.y * .01f;
        _thetaY += inputY;
    }
    /// <summary>�I�N���[�W��������</summary>
    void OcculusionSequence()
    {
        _occ.OcculusionSequence();
    }
    /// <summary>��]����</summary>
    void RotateSequence(float rotateRadius)
    {
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
        this.transform.position =
            new Vector3
            (Mathf.Cos(_thetaX) * signX
            , Mathf.Sin(_thetaY) * signY
            , Mathf.Sin(_thetaX) * signX)
            * rotateRadius
            + _centerTransform.position;
    }
    /// <summary>�v���C���[�ߑ�����</summary>
    void TargettingSequence(Transform followTransform)
    {
        //LookRotation�̑������ɐ��ʕ����̃x�N�g�����w�肵�ă^�[�Q�b�g�̃I�u�W�F�N�g������
        this.transform.rotation =
        Quaternion.LookRotation((followTransform.position - this.transform.position)
        , Vector3.up);
        //�e�����x�N�g���l������
        var f = this.transform.forward; f.y = 0;
        _forward = f;
        var r = this.transform.right; r.y = 0;
        _right = r;
    }
    #endregion
    #region public���\�b�h
    #endregion
}