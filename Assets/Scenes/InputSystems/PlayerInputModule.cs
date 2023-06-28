using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// UnityInputSystem向けのデバイス入力モジュールコンポーネント
/// </summary>
/// コーダー：菅沼
/// 備考：UnityInputSystemのBehaviourがInvokeUnityEventになっていることを確認すること。これにしか最適化していない。
public class PlayerInputModule : MonoBehaviour
{
    /* プロパティ 必要に応じて行追加行削除するのOK */
    PlayerInput _playerInput;//NULLチェック用
    Vector2 _moveInput = Vector2.zero;
    Vector2 _lookInput = Vector2.zero;
    bool _isFiring = false;
    bool _isAiming = false;
    bool _isJumping = false;

    void Awake()
    {
        #region  PlayerInputのチェックと代入
        this._playerInput
            = this.gameObject.TryGetComponent<PlayerInput>(out PlayerInput playerInput) ? playerInput : null;
        #endregion
        #region  PlayerInput以外の変数の初期化
        this._isFiring = false;
        this._moveInput = this._lookInput = Vector2.zero;
        #endregion
    }
    #region  -入力受け取り関数実装部-
    public void OnMove(InputAction.CallbackContext callbackContext)
    {
        this._moveInput = callbackContext.ReadValue<Vector2>() != null ? callbackContext.ReadValue<Vector2>() : Vector2.zero;
        print("MOVE");
    }
    public void OnLook(InputAction.CallbackContext callbackContext)
    {
        this._lookInput = callbackContext.ReadValue<Vector2>() != null ? callbackContext.ReadValue<Vector2>() : Vector2.zero;
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
    public void OnJump(InputAction.CallbackContext callbackContext)
    {
        this._isJumping = callbackContext.ReadValueAsButton();
        print("JUMP");
    }
    #endregion

    #region  入力値取得用関数
    /// <summary>
    /// 移動入力の値を返す
    /// </summary>
    /// <returns></returns>
    public Vector2 GetMoveInput()
    {
        return this._moveInput;
    }
    /// <summary>
    /// 視点移動の値を返す
    /// </summary>
    /// <returns></returns>
    public Vector2 GetLookInput()
    {
        return this._lookInput;
    }
    /// <summary>
    /// 発射入力の値を返す
    /// </summary>
    /// <returns></returns>
    public bool GetFiring()
    {
        return (this._isFiring);
    }
    /// <summary>
    /// エイム入力の値を返す
    /// </summary>
    /// <returns></returns>
    public bool GetAiming()
    {
        return (this._isAiming);
    }
    /// <summary>
    /// ジャンプの入力値を返す
    /// </summary>
    /// <returns></returns>
    public bool GetJumping()
    {
        return (this._isJumping);
    }
    #endregion
}
