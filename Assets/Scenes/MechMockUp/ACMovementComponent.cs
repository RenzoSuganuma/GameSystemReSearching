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
    bool _isHovering = false;
    bool _isGrounded = true;
    private void Awake()
    {
        //入力ハンドラ取得
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
        //カーソルロック
        Cursor.lockState = CursorLockMode.Locked;
        //コンポーネント取得
        _rb = this.GetComponent<Rigidbody>();
        //カメラコンポーネント取得
        _acCam = GameObject.FindAnyObjectByType<ACCAMComponent>();
    }
    private void FixedUpdate()
    {
        ACMoveSequence();
        ACHoveringSequence(_input.IsJumpHolding);
    }
    #region FixedUpdate内で呼び出し
    void ACMoveSequence()
    {
        //移動処理
        _rb.AddForce(this.transform.forward * _moveSpeed * _input.MoveInput.y);
        _rb.AddForce(this.transform.right * _moveSpeed * _input.MoveInput.x);
        //カメラの正面を向く
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
    #region デバイス入力イベント
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