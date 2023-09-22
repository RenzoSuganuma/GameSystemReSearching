using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class PlayerMover : MonoBehaviour
{
    Rigidbody _rb = default;
    float _jumpFrc= 10f;
    float _moveSpd = 5f;
    Vector2 _input2d = Vector2.zero;
    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.freezeRotation = true;
    }
    private void Update()
    {
        _input2d.y = Input.GetAxis("Vertical");
        _input2d.x = Input.GetAxis("Horizontal");
    }
    private void FixedUpdate()
    {
        if (_input2d != Vector2.zero)
        {
            _rb.WakeUp();
            Vector3 moveVec3d = new Vector3(_input2d.x, 0f, _input2d.y);
            moveVec3d.Normalize();
            moveVec3d *= _moveSpd;
            _rb.AddForce(moveVec3d, ForceMode.Impulse);
        }
        else
        {
            _rb.Sleep();
        }
    }
}