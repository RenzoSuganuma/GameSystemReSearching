using System;
using UnityEngine;
using UnityEngine.InputSystem;
[RequireComponent(typeof(PlayerInput))]
public class ACInputHandler : MonoBehaviour, AC_Input.IPlayerActions
{
    //����J�v���p�e�B
    PlayerInput _input;
    Vector2 _move = Vector2.zero;
    Vector2 _look = Vector2.zero;
    bool _isJumpHolding = false;
    bool _isLfire = false;
    bool _isLShift = false;
    bool _isRFire = false;
    bool _isRShift = false;
    bool _isReload = false;
    //���J�v���p�e�B
    /// <summary>�ړ�����</summary>
    public Vector2 MoveInput => _move.normalized;
    /// <summary>���_�ړ�����</summary>
    public Vector2 LookInput => _look.normalized;
    /// <summary>�W�����v�z�[���h����</summary>
    public bool IsJumpHolding => _isJumpHolding;
    public bool IsLfire => _isLfire;
    public bool ISLShift => _isLShift;
    public bool IsRFire => _isRFire;
    public bool IsRShift => _isRShift;
    public bool IsReload => _isReload;
    //���J�C�x���g
    /// <summary>�W�����v���͎��C�x���g</summary>
    public event Action Jump = () => { Debug.Log("Jump"); };
    /// <summary>�W�����v���̓z�[���h���C�x���g</summary>
    public event Action JumpHold = () => { Debug.Log("JumpHold"); };
    /// <summary>�W�����v���̓z�[���h�������C�x���g</summary>
    public event Action JumpHoldQuit = () => { Debug.Log("JumpHoldQuit"); };
    /// <summary>�T�C�h�W�����v���͎��C�x���g</summary>
    public event Action SideJump = () => { Debug.Log("SideJump"); };
    /// <summary>���r���̓C�x���g</summary>
    public event Action LFire = () => { Debug.Log("LFire"); };
    /// <summary>�������̓C�x���g</summary>
    public event Action LShift = () => { Debug.Log("LShift"); };
    /// <summary>�E�r���̓C�x���g</summary>
    public event Action RFire = () => { Debug.Log("RFire"); };
    /// <summary>�E�����̓C�x���g</summary>
    public event Action RShift = () => { Debug.Log("RShift"); };
    /// <summary>���b�N�I�����̓C�x���g</summary>
    public event Action LockOnAssist = () => { Debug.Log("LockOnAssist"); };
    void Awake()
    {
        _input = GetComponent<PlayerInput>();
        _input.notificationBehavior = PlayerNotifications.InvokeCSharpEvents;
        GameObject.DontDestroyOnLoad(this.gameObject);
    }
    void OnEnable()
    {
        _input.onActionTriggered += OnMove;
        _input.onActionTriggered += OnLook;
        _input.onActionTriggered += OnJump;
        _input.onActionTriggered += OnSideJump;
        _input.actions.FindAction("JumpHold").performed += OnJumpHold;
        _input.actions.FindAction("JumpHold").canceled += OnJumpHoldQuit;
        _input.onActionTriggered += OnLFire;
        _input.onActionTriggered += OnLShift;
        _input.onActionTriggered += OnRFire;
        _input.onActionTriggered += OnRShift;
        _input.actions.FindAction("LockOn").performed += OnLockOn;
        _input.onActionTriggered += OnReload;
    }
    void OnDisable()
    {
        _input.onActionTriggered -= OnMove;
        _input.onActionTriggered -= OnLook;
        _input.onActionTriggered -= OnJump;
        _input.onActionTriggered -= OnSideJump;
        _input.actions.FindAction("JumpHold").performed -= OnJumpHold;
        _input.actions.FindAction("JumpHold").canceled -= OnJumpHoldQuit;
        _input.onActionTriggered -= OnLFire;
        _input.onActionTriggered -= OnLShift;
        _input.onActionTriggered -= OnRFire;
        _input.onActionTriggered -= OnRShift;
        _input.actions.FindAction("LockOn").performed -= OnLockOn;
        _input.onActionTriggered -= OnReload;
    }
    public void OnMove(InputAction.CallbackContext context)//�ړ�
    {
        if (context.action.name == "Move")
        {
            _move = context.ReadValue<Vector2>().normalized;
        }
    }
    public void OnLook(InputAction.CallbackContext context)//���_�ړ�
    {
        if (context.action.name == "Look")
        {
            _look = context.ReadValue<Vector2>().normalized;
        }
    }
    public void OnJump(InputAction.CallbackContext context)//�W�����v
    {
        if (context.action.name == "Jump" && context.ReadValueAsButton())
        {
            Jump();
        }
    }
    public void OnSideJump(InputAction.CallbackContext context)//�T�C�h�W�����v
    {
        if (context.action.name == "SideJump" && context.ReadValueAsButton())
        {
            SideJump();
        }
    }
    public void OnJumpHold(InputAction.CallbackContext context)//�z�o�����O
    {
        if (context.action.name == "JumpHold" && context.ReadValueAsButton())
        {
            Debug.Log("JumpHold");
            JumpHold();
            _isJumpHolding = true;
        }
    }
    public void OnJumpHoldQuit(InputAction.CallbackContext context)//�z�o�����O����
    {
        if (context.action.name == "JumpHold" && context.action.WasReleasedThisFrame())
        {
            Debug.Log("JumpHoldQuit");
            JumpHoldQuit();
            _isJumpHolding = false;
        }
    }
    public void OnLFire(InputAction.CallbackContext context)//���r
    {
        if (context.action.name == "LFire" && context.ReadValueAsButton())
        {
            LFire();
        }
        if (context.action.name == "LFire")
        {
            _isLfire = context.ReadValueAsButton();
        }
    }
    public void OnLShift(InputAction.CallbackContext context)//����
    {
        if (context.action.name == "LShift" && context.ReadValueAsButton())
        {
            LShift();
        }
        if (context.action.name == "LShift")
        {
            _isLShift = context.ReadValueAsButton();
        }
    }
    public void OnRFire(InputAction.CallbackContext context)//�E�r
    {
        if (context.action.name == "RFire" && context.ReadValueAsButton())
        {
            RFire();
        }
        if (context.action.name == "RFire")
        {
            _isRFire = context.ReadValueAsButton();
        }
    }
    public void OnRShift(InputAction.CallbackContext context)//�E��
    {
        if (context.action.name == "RShift" && context.ReadValueAsButton())
        {
            RShift();
        }
        if (context.action.name == "RShift")
        {
            _isRShift = context.ReadValueAsButton();
        }
    }
    public void OnLockOn(InputAction.CallbackContext context)//���b�N�I�� �^�[�Q�b�g�A�V�X�g ������
    {
        if (context.action.name == "LockOn" && context.ReadValueAsButton())
        {
            LockOnAssist();
        }
    }
    public void OnReload(InputAction.CallbackContext context)
    {
        if (context.action.name == "Reload")
        {
            _isReload = context.ReadValueAsButton();
        }
    }
}