using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>ACの移動コンポーネント</summary>
[RequireComponent(typeof(Rigidbody))]
public class ACMovementComponent : MonoBehaviour
{
    Rigidbody _rb;
    ACInputHandler _input;
    ACCAMComponent _acCam;
    [SerializeField] float _moveSpeed;
    [SerializeField] float _jumpForce;
    private void Awake()
    {
        //入力ハンドラ取得
        _input = GameObject.FindAnyObjectByType<ACInputHandler>();
    }
    private void OnEnable()
    {
        _input.Jump += ACJumpSequence;
        _input.JumpHold += ACHoverSequence;
    }
    private void OnDisable()
    {
        _input.Jump -= ACJumpSequence;
        _input.JumpHold -= ACHoverSequence;
    }
    private void Start()
    {
        //カーソルロック
        Cursor.lockState = CursorLockMode.Locked;
        //コンポーネント取得
        _rb = this.GetComponent<Rigidbody>();
        //カメラコンポーネント取得
        _acCam = GameObject.FindAnyObjectByType<ACCAMComponent>();
    }
    private void FixedUpdate()
    {
        //移動処理
        _rb.AddForce(this.transform.forward * _moveSpeed * _input.MoveInput.y);
        _rb.AddForce(this.transform.right * _moveSpeed * _input.MoveInput.x);
        //カメラの正面を向く
        this.transform.forward = _acCam.Forward;
    }
    void ACJumpSequence()
    { 
        //_rb.AddForce(this.transform.up * _jumpForce, ForceMode.Impulse);
    }
    void ACHoverSequence()
    { 
        _rb.AddForce(this.transform.up * _jumpForce, ForceMode.Force);
    }
}
