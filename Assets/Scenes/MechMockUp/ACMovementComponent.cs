using UnityEngine;
using DebugLogRecorder;
using DGW;
/// <summary>ACの移動コンポーネント</summary>
[RequireComponent(typeof(Rigidbody))]
public class ACMovementComponent : MonoBehaviour
{
    Rigidbody _rb;
    /// <summary>入力ハンドラー</summary>
    ACInputHandler _input;
    /// <summary>ランタイムログ</summary>
    RuntimeLogComponent _log;
    /// <summary>カスタムメソッドが使えるクラス</summary>
    CustomMethods _cutomMethods;
    /// <summary>オービタルカメラクラス</summary>
    OrbitalCameraComponent _orbitCam;
    /// <summary>移動速度</summary>
    [SerializeField] float _moveForce;
    /// <summary>ジャンプ力</summary>
    [SerializeField] float _jumpForce;
    /// <summary>速度最大値</summary>
    [SerializeField] float _velocityLim;
    /// <summary>滞空してるかのフラグ</summary>
    bool _isHovering = false;
    /// <summary>滞空してるかのフラグ</summary>>
    public bool IsHovering => _isHovering;
    /// <summary>接地してるかのフラグ</summary>
    bool _isGrounded = true;
    /// <summary>接地してるかのフラグ</summary>
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
        _log = new(new Rect(0, 0, 500, 250));
        _cutomMethods = new();
    }
    private void FixedUpdate()
    {
        ACMoveSequence();
        ACHoveringSequence(_input.IsJumpHolding);
    }
    #region FixedUpdate内で呼び出し
    void ACMoveSequence()
    {
        _rb.AddForce(-this.transform.up * 80);
        _rb.AddForce(this.transform.forward * _moveForce * _input.MoveInput.y);
        _rb.AddForce(this.transform.right * _moveForce * _input.MoveInput.x);
        _cutomMethods.When(_rb.velocity.magnitude > _velocityLim, () => _rb.velocity = _rb.velocity.normalized * _velocityLim);
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
        _rb.AddForce(this.transform.up * _jumpForce * 1.5f, ForceMode.Impulse);
        _rb.AddForce(this.transform.right * _input.MoveInput.x * _jumpForce * 5, ForceMode.Impulse);
        _rb.AddForce(this.transform.forward * _input.MoveInput.y * _jumpForce * 5, ForceMode.Impulse);
    }
    #endregion
    #region publicメソッド
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