using UnityEngine;
using UnityEngine.InputSystem;

/* ��� */
/*
 * �P�̃e�X�g
 * �쐬�FR����
 */

/* �d�l�\�z */
/*
 * �ŏI�I�ɂ܂Ƃ߂Ĉ�̖��O��ԂɏW�񂷂�H��������Ȃ��̂Ō��S�ň��S���̍����R�[�h�ɂ���(1)
 * �ł��邾���V���v���ȏ����ŏI��点��(2)
 * InputSystem��UnityEvent����̏��œ����悤�ɂ������B(3)
 */

/* �R�[�h���� */
/* Unity��InputSystem��InvokeUnityEvents�̍��ڂ�ݒ肵��UnityEvent�ł̓��͒l�̓ǂݍ��݁A�󂯎��ɍœK�����Ă���
 * -�ȉ����͎󂯎��֐���������-�ƕ\�L����Ă��鏊��#region ���� #endregion�̊Ԃ͉���OK����ȊO��R�����ȊO�_��
 */

/* �����t���[ */
/*
 * public void �֐��ł� InputAction.CallbackContext�^�ł̓��͒l����ǂݍ���
 */

public class PlayerInputModule : MonoBehaviour
{
    /* �v���p�e�B �K�v�ɉ����čs�ǉ��s�폜�����OK */
    PlayerInput _playerInput;//NULL�`�F�b�N�p
    public Vector2 _moveVelocity { get; private set; } = Vector2.zero;
    public Vector2 _lookVelocity { get; private set; } = Vector2.zero;
    public bool _isFiring { get; private set; } = false;
    public bool _isAiming { get; private set; } = false;

    private void Awake()
    {
        #region  PlayerInput�̃`�F�b�N�Ƒ��

        this._playerInput
            = this.gameObject.TryGetComponent<PlayerInput>(out PlayerInput playerInput) ? playerInput : null;

        #endregion

        #region  PlayerInput�ȊO�̕ϐ��̏�����

        this._isFiring = false;
        this._moveVelocity = this._lookVelocity = Vector2.zero;

        #endregion
    }

    #region  -�ȉ����͎󂯎��֐���������-

    public void OnMove(InputAction.CallbackContext callbackContext)
    {
        this._moveVelocity = callbackContext.ReadValue<Vector2>() != null ? callbackContext.ReadValue<Vector2>() : Vector2.zero;
        print("MOVE");
    }
    
    public void OnLook(InputAction.CallbackContext callbackContext)
    {
        this._lookVelocity = callbackContext.ReadValue<Vector2>() != null ? callbackContext.ReadValue<Vector2>() : Vector2.zero;
        print("LOOK");
    }

    public void OnFire(InputAction.CallbackContext callbackContext)
    {
        this._isFiring = callbackContext.ReadValueAsButton();
        print("FIRE");
    }
    
    public void OnAim(InputAction.CallbackContext callbackContext)
    {
        this._isAiming = callbackContext.ReadValueAsButton();
        print("AIM");
    }

    #endregion
}