using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// ���J�𓮂������߂̃X�N���v�g�̃x�[�X���f��By����
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class MechControllerCore : MonoBehaviour
{
    /// <summary>�Q�[���V�[���ɔz�u����Ă�f�o�C�X���͗p�N���X���i�[���Ă���I�u�W�F�N�g�̃f�o�C�X���͗p�N���X���A�^�b�`</summary>
    [SerializeField,Header("�f�o�C�X���̓N���X��ۗL���Ă�I�u�W�F�N�g���A�^�b�`")] PlayerInputModule _playerInputModule = null;
    #region ���͒l�v���p�e�B�Q
    Vector2
        /// <summary>�ړ����͒l</summary>
        _moveVector2 = Vector2.zero,
        /// <summary>���_�ړ����͒l</summary>
         _lookVector2 = Vector2.zero;
    bool 
        /// <summary>���C���͒l</summary>
        _isFiring = false,
        /// <summary>�Ə����͒l</summary>
        _isAiming = false,
        /// <summary>�������͒l</summary>
        _isJumping = false;
    #endregion
    #region  �f�o�b�O�p�ϐ��Q
    [SerializeField,Header("�f�o�b�O�p�̃��O������ɕ\��")] bool _isDebugging = false;
    #endregion
    #region  �v���p�e�B�Q
    [SerializeField,Header("�L�����ړ����x"),Range(1f,100f)] float _moveSpeed = 1f;
    [SerializeField,Header("�L�������_�ړ����x"),Range(1f,100f)] float _lookSpeed = 1f;
    [SerializeField, Header("�L�����̈ړ��̎d���̃p�^�[��")] CharacterMoveMode _charaMoveMode = CharacterMoveMode.RespectPhysic;
    enum CharacterMoveMode { RespectPhysic, IgnorePysics, Hybrid }
    Rigidbody _rigidBody;
    #endregion
    void OnEnable()
    {
        //�f�o�C�X���͗p�̃R���|�[�l���g���擾�ł��Ă��Ȃ� -> �f�o�C�X���͗p�̃N���X�̃A�^�b�`������ĂȂ��Ƃ��ɗ�O�𓊂���
        if (this._playerInputModule == null) { throw new System.Exception("�f�o�C�X���͗p�̃N���X�̎擾�Ɏ��s"); }
        //�}�E�X�J�[�\���̃��b�N
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Start()
    {
        //Rigidbody���擾�ł����Ȃ炻�̂܂܃Q�b�g
        this._rigidBody = (this.gameObject.TryGetComponent<Rigidbody>(out this._rigidBody)) ? this._rigidBody : null;
    }

    void Update()
    {
        #region  �e�f�o�C�X���͒l�擾�����Q
        this._moveVector2 = this._playerInputModule.GetMoveInput().normalized;//�x�N�g���̐��K��
        this._lookVector2 = this._playerInputModule.GetLookInput().normalized;//�x�N�g���̐��K��
        this._isFiring = this._playerInputModule.GetFiring();
        this._isAiming = this._playerInputModule.GetAiming();
        this._isJumping = this._playerInputModule.GetJumping();
        #endregion
    }

    void FixedUpdate()
    {
        CharacterMove();
        CharacterRotate();
    }

    private void OnGUI()
    {
        #region  �f�o�b�O�pGUI�\������̏����Q
        string debugText = $"{this._moveVector2} : {nameof(this._moveVector2)} \n" +
                           $"{this._lookVector2} : {nameof(this._lookVector2)} \n" +
                           $"{this._isFiring} : {nameof(this._isFiring)} \n" + 
                           $"{this._isAiming} : {nameof(this._isAiming)} \n" +
                           $"{this._isJumping} : {nameof(this._isJumping)} \n";
        //�f�o�b�O�\�����邩���Ȃ����̑I��l��true�̂Ƃ��f�o�b�O����\��
        if (this._isDebugging)
        {
            GUI.TextArea(new Rect(10, 10, 200, 100), debugText);//������N���X�̃u���b�N�̒��Ɏ��߂Ă����OK�ق��̊֐����͂������NG
        }
        #endregion
    }
    #region  �L��������Ȃǂɒ�������֐��Q
    /// <summary>
    /// �L�����ړ��֐�
    /// </summary>
    void CharacterMove()
    {
        Vector3 moveVel3 = 
            (this.gameObject.transform.forward * this._moveVector2.y 
                + this.gameObject.transform.right * this._moveVector2.x);//�L�����̐��ʂƑ��ʂ̃x�N�g���̍���
        moveVel3 = moveVel3.normalized * this._moveSpeed;//���ʂƑ��ʂ̍����������x�N�g���̐��K�������Ĉړ����x�������Ă���
        Debug.Log($"{moveVel3} : {nameof(moveVel3)}");

        if (this._charaMoveMode == CharacterMoveMode.RespectPhysic || this._charaMoveMode == CharacterMoveMode.Hybrid)//�����I�ȋ�����������Ƃ�
        {
            this._rigidBody.AddForce(moveVel3 * Time.deltaTime, ForceMode.Impulse);
        }
        else if (this._charaMoveMode == CharacterMoveMode.IgnorePysics)//�����I�������������ɉ^�p����Ƃ�
        {
            if (moveVel3 != Vector3.zero)
            {
                this._rigidBody.WakeUp();
                this._rigidBody.velocity = moveVel3;
            }
            else
            {
                this._rigidBody.Sleep();
            }
        }
    }
    /// <summary>
    /// �L�����̉�]�֐�
    /// </summary>
    void CharacterRotate()
    {
        Vector3 rotTorq3 = ( (this._lookVector2.x != 0 && this._lookVector2.x < -.5f) 
            || (this._lookVector2.x != 0 && this._lookVector2.x > .5f) ) 
                ? this.gameObject.transform.up * this._lookVector2.x * this._lookSpeed
                    : Vector3.zero;
        if (this._charaMoveMode == CharacterMoveMode.RespectPhysic)//�����I�ȋ�����������Ƃ�
        {
            this._rigidBody.AddTorque( (rotTorq3 * Time.deltaTime) / 3f);
        }
        else if (this._charaMoveMode == CharacterMoveMode.IgnorePysics || this._charaMoveMode == CharacterMoveMode.Hybrid)//�����I�������������ɉ^�p����Ƃ�
        {
            if (rotTorq3 != Vector3.zero)
            {
                this._rigidBody.gameObject.transform.Rotate(rotTorq3 * Time.deltaTime);
            }
            else
            {
                this._rigidBody.gameObject.transform.Rotate(Vector3.zero);
            }
        }
    }
    #endregion
}