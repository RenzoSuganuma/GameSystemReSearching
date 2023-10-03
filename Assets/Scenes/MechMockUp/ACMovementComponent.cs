using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DebugLogRecorder;
/// <summary>AC�̈ړ��R���|�[�l���g</summary>
[RequireComponent(typeof(Rigidbody))]
public class ACMovementComponent : MonoBehaviour
{
    Rigidbody _rb;
    /// <summary>���̓n���h���[</summary>
    ACInputHandler _input;
    /// <summary>�J����</summary>
    ACCAMComponent _acCam;
    /// <summary>�����^�C�����O</summary>
    RuntimeLogComponent _log;
    /// <summary>�ړ����x</summary>
    [SerializeField] float _moveForce;
    /// <summary>�W�����v��</summary>
    [SerializeField] float _jumpForce;
    /// <summary>���x�ő�l</summary>
    [SerializeField] float _velocityLim;
    /// <summary>�؋󂵂Ă邩�̃t���O</summary>
    bool _isHovering = false;
    /// <summary>�؋󂵂Ă邩�̃t���O</summary>>
    public bool IsHovering => _isHovering;
    /// <summary>�ڒn���Ă邩�̃t���O</summary>
    bool _isGrounded = true;
    /// <summary>�ڒn���Ă邩�̃t���O</summary>
    public bool IsGrounded => _isGrounded;
    private void Awake()
    {
        _input = GameObject.FindAnyObjectByType<ACInputHandler>();
    }
    private void OnEnable()
    {
        _input.Jump += ACJumpSequence;
        _input.SideJump += ACSideJumpSequence;
    }
    private void OnDisable()
    {
        _input.Jump -= ACJumpSequence;
        _input.SideJump -= ACSideJumpSequence;
    }
    private void Start()
    {
        this.gameObject.tag = "Player";
        Cursor.lockState = CursorLockMode.Locked;
        _rb = this.GetComponent<Rigidbody>();
        _acCam = GameObject.FindAnyObjectByType<ACCAMComponent>();
        _log = new(new Rect(0, 0, 500, 250));
    }
    private void FixedUpdate()
    {
        ACMoveSequence();
        ACHoveringSequence(_input.IsJumpHolding);
    }
    #region FixedUpdate���ŌĂяo��
    void ACMoveSequence()
    {
        this.transform.forward = _acCam.Forward;
        _rb.AddForce(-this.transform.up * 80);
        if (_rb.velocity.magnitude > _velocityLim)
        {
            _rb.velocity = _rb.velocity.normalized * _velocityLim;
        }
        if (!_acCam.IsTargetAssisting)
        {
            _rb.AddForce(this.transform.forward * _moveForce * _input.MoveInput.y);
            _rb.AddForce(this.transform.right * _moveForce * _input.MoveInput.x);
        }
        else
        {
            _rb.AddForce(_acCam.Forward * _moveForce * _input.MoveInput.y);
            _rb.AddForce(_acCam.transform.right * _moveForce * _input.MoveInput.x);
        }
    }
    void ACHoveringSequence(bool isHovering)
    {
        _isHovering = isHovering;
        if (isHovering && !_isGrounded)
        {
            _rb.AddForce(this.transform.up * _jumpForce * 5, ForceMode.Force);
        }
    }
    void ACBrakeSequence()
    {
        _rb.Sleep();
        _rb.velocity = _rb.velocity * -.75f;
        _rb.WakeUp();
    }
    #endregion
    #region �f�o�C�X���̓C�x���g
    void ACJumpSequence()
    {
        if (_isGrounded)
        {
            _rb.AddForce(this.transform.up * _jumpForce, ForceMode.Impulse);
        }
    }
    void ACSideJumpSequence()
    {
        _rb.AddForce(this.transform.up * _jumpForce * 1.5f, ForceMode.Impulse);
        _rb.AddForce(this.transform.right * _input.MoveInput.x * _jumpForce * 5, ForceMode.Impulse);
        _rb.AddForce(this.transform.forward * _input.MoveInput.y * _jumpForce * 5, ForceMode.Impulse);
    }
    #endregion
    #region public���\�b�h
    public void AssignACForward(Vector3 forward)
    {
        this.transform.forward = forward;
    }
    #endregion
    private void OnGUI()
    {
        _log.DisplayLog($"RB-MAG:{_rb.velocity.magnitude}" +
            $"\nHEIGHT:{this.transform.position.y}" +
            $"\nRB-VEL{_rb.velocity}");
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            _isGrounded = true;
            ACBrakeSequence();
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            _isGrounded = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            _isGrounded = false;
        }
    }
}