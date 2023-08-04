using UnityEngine;
/*�v���C���[�L�����ɂ�����A�^�b�`���邱��*/
[RequireComponent(typeof(Rigidbody),typeof(CharacterController),typeof(CapsuleCollider))]
public class ThirdPersonController : MonoBehaviour
{
    /// <summary>�v���C���[��ǐՂ���J����</summary>
    [SerializeField, Header("�v���C���[�J����")] GameObject _playerCamera;
    /// <summary>�f�o�C�X���͂̒l��ێ����Ă���N���X</summary>
    [SerializeField, Header("�f�o�C�X���͂̒l��ێ����Ă���N���X")] PlayerInputModule _deviceInput;
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
    void Start()
    {
        /*CharacterController ����null �̎��̂ݑ������*/
        this._charCont = GetComponent<CharacterController>();
    }
    private void Update()
    {
        GetInputsVal();
        CameraRotationSequence();
    }

    void FixedUpdate()
    {
        //���͒l�ɉ����Ĉړ��A�J�����̌����Ă�����������ʂŃJ�����̓t���[���b�N
        Vector3 vMove = this.gameObject.transform.forward * this._moveInput.y + this.gameObject.transform.right * this._moveInput.x;
        this._charCont.Move(vMove);
    }
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

    private void CameraRotationSequence()
    {
        float co_x = 0, co_z = 0;
        this._camRotTheta += this._lookInput.x * Time.deltaTime;
        /*X,Z���ł̉~�̋O�Ղ����ǂ点��*/
        co_x = Mathf.Cos(_camRotTheta) * this._cameraDistance + this.gameObject.transform.position.x;
        co_z = Mathf.Sin(_camRotTheta) * this._cameraDistance + this.gameObject.transform.position.z;        
        this._playerCamera.transform.position = new Vector3(co_x, this._cameraOffsetY, co_z);
        /*�v���C���[���J�����͏�Ɍ���*/
        this._playerCamera.transform.LookAt(this.gameObject.transform.position);
    }
}
