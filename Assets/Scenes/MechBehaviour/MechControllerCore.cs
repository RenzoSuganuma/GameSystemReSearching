using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// メカを動かすためのスクリプトのベースモデルBy菅沼
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class MechControllerCore : MonoBehaviour
{
    /// <summary>ゲームシーンに配置されてるデバイス入力用クラスを格納しているオブジェクトのデバイス入力用クラスをアタッチ</summary>
    [SerializeField,Header("デバイス入力クラスを保有してるオブジェクトをアタッチ")] PlayerInputModule _playerInputModule = null;
    #region 入力値プロパティ群
    Vector2
        /// <summary>移動入力値</summary>
        _moveVector2 = Vector2.zero,
        /// <summary>視点移動入力値</summary>
         _lookVector2 = Vector2.zero;
    bool 
        /// <summary>発砲入力値</summary>
        _isFiring = false,
        /// <summary>照準入力値</summary>
        _isAiming = false,
        /// <summary>跳躍入力値</summary>
        _isJumping = false;
    #endregion
    #region  デバッグ用変数群
    [SerializeField,Header("デバッグ用のログを左上に表示")] bool _isDebugging = false;
    #endregion
    #region  プロパティ群
    [SerializeField,Header("キャラ移動速度"),Range(1f,100f)] float _moveSpeed = 1f;
    [SerializeField,Header("キャラ視点移動速度"),Range(1f,100f)] float _lookSpeed = 1f;
    [SerializeField, Header("キャラの移動の仕方のパターン")] CharacterMoveMode _charaMoveMode = CharacterMoveMode.RespectPhysic;
    enum CharacterMoveMode { RespectPhysic, IgnorePysics, Hybrid }
    Rigidbody _rigidBody;
    #endregion
    void OnEnable()
    {
        //デバイス入力用のコンポーネントを取得できていない -> デバイス入力用のクラスのアタッチがされてないときに例外を投げる
        if (this._playerInputModule == null) { throw new System.Exception("デバイス入力用のクラスの取得に失敗"); }
        //マウスカーソルのロック
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Start()
    {
        //Rigidbodyを取得できたならそのままゲット
        this._rigidBody = (this.gameObject.TryGetComponent<Rigidbody>(out this._rigidBody)) ? this._rigidBody : null;
    }

    void Update()
    {
        #region  各デバイス入力値取得処理群
        this._moveVector2 = this._playerInputModule.GetMoveInput().normalized;//ベクトルの正規化
        this._lookVector2 = this._playerInputModule.GetLookInput().normalized;//ベクトルの正規化
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
        #region  デバッグ用GUI表示周りの処理群
        string debugText = $"{this._moveVector2} : {nameof(this._moveVector2)} \n" +
                           $"{this._lookVector2} : {nameof(this._lookVector2)} \n" +
                           $"{this._isFiring} : {nameof(this._isFiring)} \n" + 
                           $"{this._isAiming} : {nameof(this._isAiming)} \n" +
                           $"{this._isJumping} : {nameof(this._isJumping)} \n";
        //デバッグ表示するかしないかの選択値がtrueのときデバッグ情報を表示
        if (this._isDebugging)
        {
            GUI.TextArea(new Rect(10, 10, 200, 100), debugText);//これをクラスのブロックの中に収めていればOKほかの関数内はもちろんNG
        }
        #endregion
    }
    #region  キャラ操作などに直結する関数群
    /// <summary>
    /// キャラ移動関数
    /// </summary>
    void CharacterMove()
    {
        Vector3 moveVel3 = 
            (this.gameObject.transform.forward * this._moveVector2.y 
                + this.gameObject.transform.right * this._moveVector2.x);//キャラの正面と側面のベクトルの合成
        moveVel3 = moveVel3.normalized * this._moveSpeed;//正面と側面の合成をしたベクトルの正規化をして移動速度をかけている
        Debug.Log($"{moveVel3} : {nameof(moveVel3)}");

        if (this._charaMoveMode == CharacterMoveMode.RespectPhysic || this._charaMoveMode == CharacterMoveMode.Hybrid)//物理的な挙動をさせるとき
        {
            this._rigidBody.AddForce(moveVel3 * Time.deltaTime, ForceMode.Impulse);
        }
        else if (this._charaMoveMode == CharacterMoveMode.IgnorePysics)//物理的挙動をさせずに運用するとき
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
    /// キャラの回転関数
    /// </summary>
    void CharacterRotate()
    {
        Vector3 rotTorq3 = ( (this._lookVector2.x != 0 && this._lookVector2.x < -.5f) 
            || (this._lookVector2.x != 0 && this._lookVector2.x > .5f) ) 
                ? this.gameObject.transform.up * this._lookVector2.x * this._lookSpeed
                    : Vector3.zero;
        if (this._charaMoveMode == CharacterMoveMode.RespectPhysic)//物理的な挙動をさせるとき
        {
            this._rigidBody.AddTorque( (rotTorq3 * Time.deltaTime) / 3f);
        }
        else if (this._charaMoveMode == CharacterMoveMode.IgnorePysics || this._charaMoveMode == CharacterMoveMode.Hybrid)//物理的挙動をさせずに運用するとき
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