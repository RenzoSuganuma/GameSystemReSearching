using System;
using UnityEngine;
using UnityEngine.InputSystem;
[RequireComponent(typeof(PlayerInput))]
public class ACInputHandler : MonoBehaviour, AC_Input.IPlayerActions
{
    //非公開プロパティ
    PlayerInput _input;
    Vector2 _move = Vector2.zero;
    Vector2 _look = Vector2.zero;
    bool _isJumpHolding = false;
    bool _isLfire = false;
    bool _isLShift = false;
    bool _isRFire = false;
    bool _isRShift = false;
    bool _isReload = false;
    //公開プロパティ
    /// <summary>移動入力</summary>
    public Vector2 MoveInput => _move.normalized;
    /// <summary>視点移動入力</summary>
    public Vector2 LookInput => _look.normalized;
    /// <summary>ジャンプホールド入力</summary>
    public bool IsJumpHolding => _isJumpHolding;
    public bool IsLfire => _isLfire;
    public bool ISLShift => _isLShift;
    public bool IsRFire => _isRFire;
    public bool IsRShift => _isRShift;
    public bool IsReload => _isReload;
    //公開イベント
    /// <summary>ジャンプ入力時イベント</summary>
    public event Action Jump = () => { Debug.Log("Jump"); };
    /// <summary>ジャンプ入力ホールド時イベント</summary>
    public event Action JumpHold = () => { Debug.Log("JumpHold"); };
    /// <summary>ジャンプ入力ホールド解除時イベント</summary>
    public event Action JumpHoldQuit = () => { Debug.Log("JumpHoldQuit"); };
    /// <summary>サイドジャンプ入力時イベント</summary>
    public event Action SideJump = () => { Debug.Log("SideJump"); };
    /// <summary>左腕入力イベント</summary>
    public event Action LFire = () => { Debug.Log("LFire"); };
    /// <summary>左肩入力イベント</summary>
    public event Action LShift = () => { Debug.Log("LShift"); };
    /// <summary>右腕入力イベント</summary>
    public event Action RFire = () => { Debug.Log("RFire"); };
    /// <summary>右肩入力イベント</summary>
    public event Action RShift = () => { Debug.Log("RShift"); };
    /// <summary>ロックオン入力イベント</summary>
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
    public void OnMove(InputAction.CallbackContext context)//移動
    {
        if (context.action.name == "Move")
        {
            _move = context.ReadValue<Vector2>().normalized;
        }
    }
    public void OnLook(InputAction.CallbackContext context)//視点移動
    {
        if (context.action.name == "Look")
        {
            _look = context.ReadValue<Vector2>().normalized;
        }
    }
    public void OnJump(InputAction.CallbackContext context)//ジャンプ
    {
        if (context.action.name == "Jump" && context.ReadValueAsButton())
        {
            Jump();
        }
    }
    public void OnSideJump(InputAction.CallbackContext context)//サイドジャンプ
    {
        if (context.action.name == "SideJump" && context.ReadValueAsButton())
        {
            SideJump();
        }
    }
    public void OnJumpHold(InputAction.CallbackContext context)//ホバリング
    {
        if (context.action.name == "JumpHold" && context.ReadValueAsButton())
        {
            Debug.Log("JumpHold");
            JumpHold();
            _isJumpHolding = true;
        }
    }
    public void OnJumpHoldQuit(InputAction.CallbackContext context)//ホバリング解除
    {
        if (context.action.name == "JumpHold" && context.action.WasReleasedThisFrame())
        {
            Debug.Log("JumpHoldQuit");
            JumpHoldQuit();
            _isJumpHolding = false;
        }
    }
    public void OnLFire(InputAction.CallbackContext context)//左腕
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
    public void OnLShift(InputAction.CallbackContext context)//左肩
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
    public void OnRFire(InputAction.CallbackContext context)//右腕
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
    public void OnRShift(InputAction.CallbackContext context)//右肩
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
    public void OnLockOn(InputAction.CallbackContext context)//ロックオン ターゲットアシスト 長押し
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