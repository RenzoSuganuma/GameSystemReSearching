using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RuntimeLog;
/// <summary>ACの移動コンポーネント</summary>
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
        //ログコンポーネントインスタンス化
        _log = new(new Rect(0, 0, 500, 250));
    }
    private void FixedUpdate()
    {
        ACMoveSequence();
        ACGravityIncSequence();
        ACHoveringSequence(_input.IsJumpHolding);
    }
    #region FixedUpdate内で呼び出し
    void ACMoveSequence()
    {
        //移動処理
        _rb.AddForce(this.transform.forward * _moveSpeed * _input.MoveInput.y);
        _rb.AddForce(this.transform.right * _moveSpeed * _input.MoveInput.x);
        //速度制限
        if (_rb.velocity.magnitude > _velocityLim)
        {
            _rb.velocity = _rb.velocity.normalized * _velocityLim;
        }
        //カメラの正面を向く
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