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
    [SerializeField, Range(2f, 5f), Header("�J�����ƃv���C���[�Ԃ̋���")] float _cameraDistance;
    /// <summary>Y���̃J�����I�t�Z�b�g</summary>
    [SerializeField, Range(-5f, 5f), Header("�J�����ƃv���C���[�Ԃ̋���")] float _cameraOffsetY;
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
        this._moveInput = this._deviceInput.GetMoveInput();
        this._lookInput = this._deviceInput.GetLookInput();
        this._isFired = this._deviceInput.GetFiring();
        this._isAimed = this._deviceInput.GetAiming();
        this._isJumped = this._deviceInput.GetJumping();
        print($"M,L,F,A,J => {this._moveInput},{this._lookInput},{this._isFired},{this._isAimed},{this._isJumped}");
    }
    /// <summary>�L�����ړ�����̊֐�</summary>
    private void CharacterMoveSequence()
    {

        //���͒l�ɉ����Ĉړ��A�J�����̌����Ă�����������ʂŃJ�����̓t���[���b�N
        Vector3 moveVecFrwrd = new Vector3(this._playerCamera.transform.forward.x, 0, this._playerCamera.transform.forward.z);
        Vector3 moveVecR = new Vector3(this._playerCamera.transform.right.x, 0, this._playerCamera.transform.right.z);
        Vector3 vMove = moveVecFrwrd * this._moveInput.y + moveVecR * this._moveInput.x;
        if (this._moveInput != Vector2.zero)
        {
            this.gameObject.transform.forward = moveVecFrwrd * this._moveInput.y + moveVecR * this._moveInput.x;
        }
        this._charCont.Move(vMove);
    }
    /// <summary>�J�����̎��_�ړ��̊֐�</summary>
    private void CameraRotationSequence()
    {
        float co_x = 0, co_z = 0;
        this._camRotTheta -= this._lookInput.x * Time.deltaTime;
        /*X,Z���ł̉~�̋O�Ղ����ǂ点��*/
        co_x = Mathf.Cos(_camRotTheta) * this._cameraDistance + this.gameObject.transform.position.x;
        co_z = Mathf.Sin(_camRotTheta) * this._cameraDistance + this.gameObject.transform.position.z;        
        this._playerCamera.transform.position = new Vector3(co_x, this._cameraOffsetY, co_z);
        /*�v���C���[���J�����͏�Ɍ���*/
        this._playerCamera.transform.LookAt(this.gameObject.transform.position);
    }
}
