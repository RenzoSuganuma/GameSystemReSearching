using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class ACMovementComponent : MonoBehaviour
{
    Rigidbody _rb;
    ACInputHandler _input;
    [SerializeField] float _moveSpeed;
    private void Start()
    {
        //�J�[�\�����b�N
        Cursor.lockState = CursorLockMode.Locked;
        //�R���|�[�l���g�擾
        _rb = this.GetComponent<Rigidbody>();
        //���̓n���h���擾
        _input = GameObject.FindAnyObjectByType<ACInputHandler>();
    }
    private void FixedUpdate()
    {
        //�ړ�����
        _rb.AddForce(this.transform.forward * _moveSpeed * _input.MoveInput.y);
        _rb.AddForce(this.transform.right * _moveSpeed * _input.MoveInput.x);
    }
}
