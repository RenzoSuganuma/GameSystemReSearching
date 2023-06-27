using UnityEngine;
using UnityEngine.InputSystem;

/* 情報 */
/*
 * 単体テスト
 * 作成：R菅沼
 */

/* 仕様構想 */
/*
 * 最終的にまとめて一つの名前空間に集約する？かもしれないので堅牢で安全性の高いコードにする(1)
 * できるだけシンプルな処理で終わらせる(2)
 * InputSystemのUnityEventからの情報で動くようにしたい。(3)
 */

/* コード説明 */
/* UnityのInputSystemのInvokeUnityEventsの項目を設定したUnityEventでの入力値の読み込み、受け取りに最適化している
 * -以下入力受け取り関数実装部分-と表記されている所の#region から #endregionの間は改変OKそれ以外はR菅沼以外ダメ
 */

/* 処理フロー */
/*
 * public void 関数での InputAction.CallbackContext型での入力値から読み込む
 */

public class PlayerInputModule : MonoBehaviour
{
    /* プロパティ 必要に応じて行追加行削除するのOK */
    PlayerInput _playerInput;//NULLチェック用
    public Vector2 _moveVelocity { get; private set; } = Vector2.zero;
    public Vector2 _lookVelocity { get; private set; } = Vector2.zero;
    public bool _isFiring { get; private set; } = false;
    public bool _isAiming { get; private set; } = false;

    private void Awake()
    {
        #region  PlayerInputのチェックと代入
        this._playerInput
            = this.gameObject.TryGetComponent<PlayerInput>(out PlayerInput playerInput) ? playerInput : null;
        #endregion
        #region  PlayerInput以外の変数の初期化
        this._isFiring = false;
        this._moveVelocity = this._lookVelocity = Vector2.zero;
        #endregion
    }
    #region  -以下入力受け取り関数実装部分-
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
