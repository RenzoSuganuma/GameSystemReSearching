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
    private void Awake()
    {
        //���̓n���h���擾
        _input = GameObject.FindAnyObjectByType<ACInputHandler>();
    }
    private void OnEnable()
    {
        _input.Jump += ACJumpSequence;
    }
    private void OnDisable()
    {
        _input.Jump -= ACJumpSequence;
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
    void ACMoveSequence()
    {
        //�ړ�����
        _rb.AddForce(this.transform.forward * _moveSpeed * _input.MoveInput.y);
        _rb.AddForce(this.transform.right * _moveSpeed * _input.MoveInput.x);
        //�J�����̐��ʂ�����
        this.transform.forward = _acCam.Forward;
    }
    void ACJumpSequence()
    {
        _rb.AddForce(this.transform.up * _jumpForce, ForceMode.Impulse);
    }
    void ACHoveringSequence(bool isHovering)
    {
        if(isHovering)
        {
            _rb.AddForce(this.transform.up * _jumpForce, ForceMode.Force);
        }
    }
}
