using UnityEngine;
/*プレイヤーキャラにこれをアタッチすること*/
[RequireComponent(typeof(Rigidbody),typeof(CharacterController),typeof(CapsuleCollider))]
public class ThirdPersonController : MonoBehaviour
{
    /// <summary>プレイヤーを追跡するカメラ</summary>
    [SerializeField, Header("プレイヤーカメラ")] GameObject _playerCamera;
    /// <summary>デバイス入力の値を保持しているクラス</summary>
    [SerializeField, Header("デバイス入力の値を保持しているクラス")] PlayerInputModule _deviceInput;/*自作のクラスに依存している*/
    /// <summary>カメラとプレイヤー間の距離</summary>
    [SerializeField, Range(2f, 5f), Header("カメラとプレイヤー間の距離")] float _cameraDistance;
    /// <summary>Y軸のカメラオフセット</summary>
    [SerializeField, Range(-5f, 5f), Header("カメラとプレイヤー間の距離")] float _cameraOffsetY;
    /// <summary>移動ベクトル</summary>
    Vector2 _moveInput = Vector2.zero;
    /// <summary>視点移動ベクトル</summary>
    Vector2 _lookInput = Vector2.zero;
    /// <summary>発射フラグ</summary>
    bool _isFired = false;
    /// <summary>照準フラグ</summary>
    bool _isAimed = false;
    /// <summary>ジャンプフラグ</summary>
    bool _isJumped = false;
    /// <summary>カメラ回転用の三角関数引数Θに相当</summary>
    float _camRotTheta = 0;
    /// <summary>キャラ操作用のコンポーネント</summary>
    CharacterController _charCont;
    private void Start()
    {
        /*CharacterController が非null の時のみ代入処理*/
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
    /// <summary>デバイス入力値の取得</summary>
    private void GetInputsVal()
    {
        /*それぞれの入力値の代入*/
        this._moveInput = this._deviceInput.GetMoveInput();
        this._lookInput = this._deviceInput.GetLookInput();
        this._isFired = this._deviceInput.GetFiring();
        this._isAimed = this._deviceInput.GetAiming();
        this._isJumped = this._deviceInput.GetJumping();
        print($"M,L,F,A,J => {this._moveInput},{this._lookInput},{this._isFired},{this._isAimed},{this._isJumped}");
    }
    /// <summary>キャラ移動制御の関数</summary>
    private void CharacterMoveSequence()
    {

        //入力値に応じて移動、カメラの向いている方向が正面でカメラはフリールック
        Vector3 moveVecFrwrd = new Vector3(this._playerCamera.transform.forward.x, 0, this._playerCamera.transform.forward.z);
        Vector3 moveVecR = new Vector3(this._playerCamera.transform.right.x, 0, this._playerCamera.transform.right.z);
        Vector3 vMove = moveVecFrwrd * this._moveInput.y + moveVecR * this._moveInput.x;
        if (this._moveInput != Vector2.zero)
        {
            this.gameObject.transform.forward = moveVecFrwrd * this._moveInput.y + moveVecR * this._moveInput.x;
        }
        this._charCont.Move(vMove);
    }
    /// <summary>カメラの視点移動の関数</summary>
    private void CameraRotationSequence()
    {
        float co_x = 0, co_z = 0;
        this._camRotTheta -= this._lookInput.x * Time.deltaTime;
        /*X,Z軸での円の軌跡をたどらせる*/
        co_x = Mathf.Cos(_camRotTheta) * this._cameraDistance + this.gameObject.transform.position.x;
        co_z = Mathf.Sin(_camRotTheta) * this._cameraDistance + this.gameObject.transform.position.z;        
        this._playerCamera.transform.position = new Vector3(co_x, this._cameraOffsetY, co_z);
        /*プレイヤーをカメラは常に向く*/
        this._playerCamera.transform.LookAt(this.gameObject.transform.position);
    }
}
