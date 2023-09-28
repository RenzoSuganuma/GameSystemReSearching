using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
[RequireComponent(typeof(PlayerInput))]
public class ACInputHandler : MonoBehaviour, AC_Input.IPlayerActions
{
    //����J�v���p�e�B
    PlayerInput _input;
    Vector2 _move = Vector2.zero;
    Vector2 _look = Vector2.zero;
    //���J�v���p�e�B
    public Vector2 MoveInput => _move;
    public Vector2 LookInput => _look;
    public event Action Jump = ()=> { Debug.Log("Jump"); };
    public event Action LFire = () => { Debug.Log("LFire"); };
    public event Action LShift = () => { Debug.Log("LShift"); };
    public event Action RFire = () => { Debug.Log("RFire"); };
    public event Action RShift = () => { Debug.Log("RShift"); };
    private void Awake()
    {
        _input = GetComponent<PlayerInput>();
        _input.notificationBehavior = PlayerNotifications.InvokeCSharpEvents;
    }
    private void OnEnable()
    {
        _input.onActionTriggered += OnMove;
        _input.onActionTriggered += OnLook;
        _input.onActionTriggered += OnJump;
        _input.onActionTriggered += OnLFire;
        _input.onActionTriggered += OnLShift;
        _input.onActionTriggered += OnRFire;
        _input.onActionTriggered += OnRShift;
    }
    private void OnDisable()
    {
        _input.onActionTriggered -= OnMove;
        _input.onActionTriggered -= OnLook;
        _input.onActionTriggered -= OnJump;
        _input.onActionTriggered -= OnLFire;
        _input.onActionTriggered -= OnLShift;
        _input.onActionTriggered -= OnRFire;
        _input.onActionTriggered -= OnRShift;
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
    public void OnLFire(InputAction.CallbackContext context)//���r
    {
        if (context.action.name == "LFire" && context.ReadValueAsButton())
        {
            LFire();
        }
    }
    public void OnLShift(InputAction.CallbackContext context)//����
    {
        if (context.action.name == "LShift" && context.ReadValueAsButton())
        {
            LShift();
        }
    }
    public void OnRFire(InputAction.CallbackContext context)//�E�r
    {
        if (context.action.name == "RFire" && context.ReadValueAsButton())
        {
            RFire();
        }
    }
    public void OnRShift(InputAction.CallbackContext context)//�E��
    {
        if (context.action.name == "RShift" && context.ReadValueAsButton())
        {
            RShift();
        }
    }
}