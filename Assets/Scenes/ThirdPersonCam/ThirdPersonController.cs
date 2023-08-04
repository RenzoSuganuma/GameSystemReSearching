using System;
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
    [SerializeField, Header("カメラとプレイヤー間の距離")] float _cameraDistance;
    private const float MinCamDistance = 2f, MaxCamDistance = 10f;
    /// <summary>Y軸のカメラオフセット</summary>
    [SerializeField, Header("カメラとプレイヤー間の距離")] float _cameraOffsetY;
    private const float MinCamYOffset = 0f, MaxCamYOffset = 10f;
    /// <summary>プレイヤーの移動速度</summary>
    [SerializeField, Header("プレイヤー移動速度")] float _playerSpeed;
    private const float MinPSpeed = 1f, MaxPSpeed = 10f;
    /// <summary>プレイヤーの視点移動速度</summary>
    [SerializeField, Header("プレイヤー視点移動速度")] float _cameraSpeed;
    private const float MinCamSpeed = 1f, MaxCamSpeed = 10f;
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
    /// <summary>マウスカーソルロックの匿名関数</summary>
    Action hidecursor = () => { Cursor.lockState = CursorLockMode.Locked; };
    private void Start()
    {
        /*CharacterController が非null の時のみ代入処理*/
        this._charCont = GetComponent<CharacterController>();
        hidecursor();
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
        this._moveInput = this._deviceInput.GetMoveInput().normalized;
        this._lookInput = this._deviceInput.GetLookInput().normalized;
        this._isFired = this._deviceInput.GetFiring();
        this._isAimed = this._deviceInput.GetAiming();
        this._isJumped = this._deviceInput.GetJumping();
        print($"M,L,F,A,J => {this._moveInput},{this._lookInput},{this._isFired},{this._isAimed},{this._isJumped}");
    }
    /// <summary>キャラ移動制御の関数</summary>
    private void CharacterMoveSequence()
    {
        Transform pCamTrs = this._playerCamera.transform;
        /*入力値に応じて移動、カメラの向いている方向が正面でカメラはフリールック*/
        /*移動時の正面のベクトル*/
        Vector3 moveVecFrwrd = new Vector3(pCamTrs.forward.x, 0, pCamTrs.forward.z);
        /*移動時の右方向のベクトル*/
        Vector3 moveVecR = new Vector3(pCamTrs.right.x, 0, pCamTrs.right.z);
        /*移動のベクトル*/
        Vector3 vMove = moveVecFrwrd * this._moveInput.y + moveVecR * this._moveInput.x;
        /*移動ベクトルの正規化*/
        vMove = vMove.normalized;
        vMove *= this._playerSpeed;
        /*移動入力があったならその方向を向く*/
        if (this._moveInput != Vector2.zero)
        {
            this.gameObject.transform.forward = moveVecFrwrd * this._moveInput.y + moveVecR * this._moveInput.x;
        }
        /*移動する*/
        this._charCont.Move(vMove);
    }
    /// <summary>カメラの視点移動の関数</summary>
    private void CameraRotationSequence()
    {
        float co_x = 0, co_z = 0;
        this._camRotTheta -= this._lookInput.x * Time.deltaTime * this._cameraSpeed;
        Transform goTrs = this.gameObject.transform, pCamTrs = this._playerCamera.transform;
        /*X,Z軸での円の軌跡をたどらせ、プレイヤーに追従させる。ここでは円の回転の中心の座標にプレイヤーの座標をそれぞれ代入している*/
        co_x = Mathf.Cos(_camRotTheta) * this._cameraDistance + goTrs.position.x;
        co_z = Mathf.Sin(_camRotTheta) * this._cameraDistance + goTrs.position.z;
        pCamTrs.position = new Vector3(co_x, this._cameraOffsetY, co_z);
        /*プレイヤーをカメラは常に向く*/
        Vector3 camLookAtVec = goTrs.position - pCamTrs.position;
        pCamTrs.forward = camLookAtVec;
    }
}