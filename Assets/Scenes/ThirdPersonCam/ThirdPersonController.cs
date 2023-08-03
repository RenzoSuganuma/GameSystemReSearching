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
    void Start()
    {
        
    }
    private void Update()
    {
        GetInputsVal();
        Vector3 playerForward = this.gameObject.transform.forward;
        playerForward = playerForward.normalized;
        playerForward.y = this.gameObject.transform.position.y + this._cameraOffsetY;
        playerForward.z = -(this.gameObject.transform.position.z + this._cameraDistance);
        this._playerCamera.transform.position = playerForward;
    }
    void FixedUpdate()
    {
        //���͒l�ɉ����Ĉړ��A�J�����̌����Ă�����������ʂŃJ�����̓t���[���b�N
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
}
