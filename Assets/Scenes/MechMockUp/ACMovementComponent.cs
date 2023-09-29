using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>AC�̈ړ��R���|�[�l���g</summary>
[RequireComponent(typeof(Rigidbody))]
public class ACMovementComponent : MonoBehaviour
{
    Rigidbody _rb;
    ACInputHandler _input;
    ACCAMComponent _acCam;
    [SerializeField] float _moveSpeed;
    [SerializeField] float _jumpForce;
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
    }
    private void FixedUpdate()
    {
        ACMoveSequence();
        ACHoveringSequence(_input.IsJumpHolding);
    }
    #region FixedUpdate���ŌĂяo��
    void ACMoveSequence()
    {
        //�ړ�����
        _rb.AddForce(this.transform.forward * _moveSpeed * _input.MoveInput.y);
        _rb.AddForce(this.transform.right * _moveSpeed * _input.MoveInput.x);
        //�J�����̐��ʂ�����
        this.transform.forward = _acCam.Forward;
    }
    void ACHoveringSequence(bool isHovering)
    {
        _isHovering = isHovering;
        if (isHovering)
        {
            _rb.AddForce(this.transform.up * _jumpForce, ForceMode.Force);
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
        _rb.AddForce(this.transform.right * _input.MoveInput.x * _jumpForce, ForceMode.Impulse);
    }
    #endregion
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            _isGrounded = true;
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