using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RuntimeLog;
/// <summary>AC�̈ړ��R���|�[�l���g</summary>
[RequireComponent(typeof(Rigidbody))]
public class ACMovementComponent : MonoBehaviour
{
    Rigidbody _rb;
    ACInputHandler _input;
    ACCAMComponent _acCam;
    RuntimeLogComponent _log;
    [SerializeField] float _moveSpeed;
    [SerializeField] float _jumpForce;
    [SerializeField] float _velocityLim;
    [SerializeField] float _hoveringTime;
    bool _isHovering = false;
    bool _isGrounded = true;
    private void Awake()
    {
        //���̓n���h���擾
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
        //�J�[�\�����b�N
        Cursor.lockState = CursorLockMode.Locked;
        //�R���|�[�l���g�擾
        _rb = this.GetComponent<Rigidbody>();
        //�J�����R���|�[�l���g�擾
        _acCam = GameObject.FindAnyObjectByType<ACCAMComponent>();
        //���O�R���|�[�l���g�C���X�^���X��
        _log = new(new Rect(0, 0, 500, 250));
    }
    private void FixedUpdate()
    {
        ACMoveSequence();
        ACGravityIncSequence();
        ACHoveringSequence(_input.IsJumpHolding);
    }
    #region FixedUpdate���ŌĂяo��
    void ACMoveSequence()
    {
        //�ړ�����
        _rb.AddForce(this.transform.forward * _moveSpeed * _input.MoveInput.y);
        _rb.AddForce(this.transform.right * _moveSpeed * _input.MoveInput.x);
        //���x����
        if (_rb.velocity.magnitude > _velocityLim)
        {
            _rb.velocity = _rb.velocity.normalized * _velocityLim;
        }
        //�J�����̐��ʂ�����
        this.transform.forward = _acCam.Forward;
    }
    void ACHoveringSequence(bool isHovering)
    {
        _isHovering = isHovering;
        if (isHovering && !_isGrounded)
        {
            _rb.AddForce(this.transform.up * _jumpForce, ForceMode.Force);
        }
    }
    void ACBrakeSequence()
    {
        _rb.Sleep();
        _rb.velocity = _rb.velocity * -.75f;
        _rb.WakeUp();
    }
    void ACGravityIncSequence()
    {
        if (_rb.velocity.y < -9.81f * _hoveringTime)
        {
            _rb.mass = 100f;
        }
        else
        {
            _rb.mass = 1;
        }
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
        _rb.AddForce(this.transform.up * _jumpForce, ForceMode.Impulse);
        _rb.AddForce(this.transform.right * _input.MoveInput.x * _jumpForce, ForceMode.Impulse);
        _rb.AddForce(this.transform.forward * _input.MoveInput.y * _jumpForce, ForceMode.Impulse);
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