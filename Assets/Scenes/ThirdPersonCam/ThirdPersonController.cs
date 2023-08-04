using UnityEngine;
/*�v���C���[�L�����ɂ�����A�^�b�`���邱��*/
[RequireComponent(typeof(Rigidbody),typeof(CharacterController),typeof(CapsuleCollider))]
public class ThirdPersonController : MonoBehaviour
{
    /// <summary>�v���C���[��ǐՂ���J����</summary>
    [SerializeField, Header("�v���C���[�J����")] GameObject _playerCamera;
    /// <summary>�f�o�C�X���͂̒l��ێ����Ă���N���X</summary>
    [SerializeField, Header("�f�o�C�X���͂̒l��ێ����Ă���N���X")] PlayerInputModule _deviceInput;/*����̃N���X�Ɉˑ����Ă���*/
    /// <summary>�J�����ƃv���C���[�Ԃ̋���</summary>
    [SerializeField, Header("�J�����ƃv���C���[�Ԃ̋���")] float _cameraDistance;
    private const float MinCamDistance = 2f, MaxCamDistance = 10f;
    /// <summary>Y���̃J�����I�t�Z�b�g</summary>
    [SerializeField, Header("�J�����ƃv���C���[�Ԃ̋���")] float _cameraOffsetY;
    private const float MinCamYOffset = 0f, MaxCamYOffset = 10f;
    /// <summary>�v���C���[�̈ړ����x</summary>
    [SerializeField, Header("�v���C���[�ړ����x")] float _playerSpeed;
    private const float MinPSpeed = 1f, MaxPSpeed = 10f;
    /// <summary>�ړ��x�N�g��</summary>
    Vector2 _moveInput = Vector2.zero;
    /// <summary>���_�ړ��x�N�g��</summary>
    Vector2 _lookInput = Vector2.zero;
    /// <summary>���˃t���O</summary>
    bool _isFired = false;
    /// <summary>�Ə��t���O</summary>
    bool _isAimed = false;
    /// <summary>�W�����v�t���O</summary>
    bool _isJumped = false;
    /// <summary>�J������]�p�̎O�p�֐��������ɑ���</summary>
    float _camRotTheta = 0;
    /// <summary>�L��������p�̃R���|�[�l���g</summary>
    CharacterController _charCont;
    private void Start()
    {
        /*CharacterController ����null �̎��̂ݑ������*/
        this._charCont = GetComponent<CharacterController>();
        /*�e�v���p�e�B�̒l�̐ݒ�*/
        Mathf.Clamp(this._cameraDistance, MinCamDistance, MaxCamDistance);
        Mathf.Clamp(this._cameraOffsetY, MinCamYOffset, MaxCamYOffset);
        Mathf.Clamp(this._playerSpeed, MinPSpeed, MaxPSpeed);
    }
    private void Update()
    {
        GetInputsVal();
        CameraRotationSequence();
    }
    private void FixedUpdate()
    {
        CharacterMoveSequence();
    }
    /// <summary>�f�o�C�X���͒l�̎擾</summary>
    private void GetInputsVal()
    {
        /*���ꂼ��̓��͒l�̑��*/
        this._moveInput = this._deviceInput.GetMoveInput().normalized;
        this._lookInput = this._deviceInput.GetLookInput().normalized;
        this._isFired = this._deviceInput.GetFiring();
        this._isAimed = this._deviceInput.GetAiming();
        this._isJumped = this._deviceInput.GetJumping();
        print($"M,L,F,A,J => {this._moveInput},{this._lookInput},{this._isFired},{this._isAimed},{this._isJumped}");
    }
    /// <summary>�L�����ړ�����̊֐�</summary>
    private void CharacterMoveSequence()
    {

        /*���͒l�ɉ����Ĉړ��A�J�����̌����Ă�����������ʂŃJ�����̓t���[���b�N*/
        /*�ړ����̐��ʂ̃x�N�g��*/
        Vector3 moveVecFrwrd = new Vector3(this._playerCamera.transform.forward.x, 0, this._playerCamera.transform.forward.z);
        /*�ړ����̉E�����̃x�N�g��*/
        Vector3 moveVecR = new Vector3(this._playerCamera.transform.right.x, 0, this._playerCamera.transform.right.z);
        /*�ړ��̃x�N�g��*/
        Vector3 vMove = moveVecFrwrd * this._moveInput.y + moveVecR * this._moveInput.x;
        /*�ړ��x�N�g���̐��K��*/
        vMove = vMove.normalized;
        vMove *= this._playerSpeed;
        /*�ړ����͂��������Ȃ炻�̕���������*/
        if (this._moveInput != Vector2.zero)
        {
            this.gameObject.transform.forward = moveVecFrwrd * this._moveInput.y + moveVecR * this._moveInput.x;
        }
        /*�ړ�����*/
        this._charCont.Move(vMove);
    }
    /// <summary>�J�����̎��_�ړ��̊֐�</summary>
    private void CameraRotationSequence()
    {
        float co_x = 0, co_z = 0;
        this._camRotTheta -= this._lookInput.x * Time.deltaTime;
        /*X,Z���ł̉~�̋O�Ղ����ǂ点�A�v���C���[�ɒǏ]������B�����ł͉~�̉�]�̒��S�̍��W�Ƀv���C���[�̍��W�����ꂼ�������Ă���*/
        co_x = Mathf.Cos(_camRotTheta) * this._cameraDistance + this.gameObject.transform.position.x;
        co_z = Mathf.Sin(_camRotTheta) * this._cameraDistance + this.gameObject.transform.position.z;        
        this._playerCamera.transform.position = new Vector3(co_x, this._cameraOffsetY, co_z);
        /*�v���C���[���J�����͏�Ɍ���*/
        Vector3 camLookAtVec = this.gameObject.transform.position - this._playerCamera.transform.position;
        this._playerCamera.transform.forward = camLookAtVec;
    }
}
