using UnityEngine;
using DebugLogRecorder;
using DGW;
/// <summary>AC�̈ړ��R���|�[�l���g</summary>
[RequireComponent(typeof(Rigidbody))]
public class MechMovementComponent : MonoBehaviour
{
    Rigidbody _rb;
    ACInputHandler _input;
    RuntimeLogComponent _log;
    CustomMethods _cutomMethods;
    /// <summary>�I�[�r�^���J�����N���X</summary>
    OrbitalCameraComponent _orbitCAM;
    /// <summary>�G�C���A�V�X�g�J�����N���X</summary>
    AimAssistCameraComponent _assistCAM;
    /// <summary>�J�����}�l�[�W���[�N���X�N���X</summary>
    ACCAMManager _camMan;
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
        _camMan = GameObject.FindAnyObjectByType<ACCAMManager>();
        _orbitCAM = GameObject.FindAnyObjectByType<OrbitalCameraComponent>();
        _assistCAM = GameObject.FindAnyObjectByType<AimAssistCameraComponent>();
        _log = new(new Rect(0, 0, 500, 250));
        _cutomMethods = new();
    }
    private void FixedUpdate()
    {
        ACMoveSequence();
        ACHoveringSequence(_input.IsJumpHolding);
        ACTurnSequence();
    }
    #region FixedUpdate���ŌĂяo��
    void ACMoveSequence()
    {
        switch (_camMan.CamMode)
        {
            case CameraMode.Normal:
                {
                    _rb.AddForce(_orbitCAM.Forward * _moveForce * _input.MoveInput.y);
                    _rb.AddForce(_orbitCAM.Right * _moveForce * _input.MoveInput.x);
                    break;
                }
            case CameraMode.AimAssist:
                {
                    _rb.AddForce(_assistCAM.Forward * _moveForce * _input.MoveInput.y);
                    _rb.AddForce(_assistCAM.Right * _moveForce * _input.MoveInput.x);
                    break;
                }
        }
        _rb.AddForce(-this.transform.up * 80);
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
    void ACTurnSequence()
    {
        if (_input.MoveInput != Vector2.zero)
        {
            switch (_camMan.CamMode)
            {
                case CameraMode.Normal:
                    {
                        this.transform.forward =
                            _orbitCAM.Forward * _input.MoveInput.y
                            + _orbitCAM.Right * _input.MoveInput.x;
                        break;
                    }
                case CameraMode.AimAssist:
                    {
                        this.transform.forward =
                        _assistCAM.Forward;
                        break;
                    }
            }
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
        switch (_camMan.CamMode)
        {
            case CameraMode.Normal:
                {
                    _rb.AddForce(_orbitCAM.Right * _input.MoveInput.x * _jumpForce * 5, ForceMode.Impulse);
                    _rb.AddForce(_orbitCAM.Forward * _input.MoveInput.y * _jumpForce * 5, ForceMode.Impulse);
                    break;
                }
            case CameraMode.AimAssist:
                {
                    _rb.AddForce(_assistCAM.Right * _input.MoveInput.x * _jumpForce * 5, ForceMode.Impulse);
                    _rb.AddForce(_assistCAM.Forward * _input.MoveInput.y * _jumpForce * 5, ForceMode.Impulse);
                    break;
                }
        }
        _rb.AddForce(this.transform.up * _jumpForce * 1.5f, ForceMode.Impulse);
    }
    #endregion
    #region public���\�b�h
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