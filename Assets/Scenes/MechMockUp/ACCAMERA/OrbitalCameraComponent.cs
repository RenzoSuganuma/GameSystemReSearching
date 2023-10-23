using System.Collections.Generic;
using UnityEngine;
using System;
using RS;
/// <summary>ACのカメラ動作コンポーネント</summary>
public class OrbitalCameraComponent : MonoBehaviour
{
    ACInputHandler _input;
    MechMovementComponent _acMove;
    /// <summary>オクルージョン処理クラス</summary>
    Occulutioner _occ;
    /// <summary>正面ベクトル</summary>
    Vector3 _forward;
    /// <summary>正面ベクトル</summary>
    public Vector3 Forward => _forward;
    /// <summary>右ベクトル</summary>
    Vector3 _right;
    /// <summary>右ベクトル</summary>
    public Vector3 Right => _right;
    /// <summary>カメラの中心座標</summary>
    [SerializeField] Transform _centerTransform;
    /// <summary>入力感度</summary>
    [SerializeField] Vector2 _sencitivity = new(1, .5f);
    /// <summary>回転半径</summary>
    [SerializeField] float _rotateRadius;
    /// <summary>X軸回転角度のクランプするときの値の絶対値</summary>
    [SerializeField, Range(.1f, .5f)] float _rollAngleAbsValue = .3f;
    /// <summary>回転の反転を有効にするかのフラグ</summary>
    [SerializeField] bool _inverseRotationY;
    /// <summary>回転の反転を有効にするかのフラグ</summary>
    [SerializeField] bool _inverseRotationX;
    /// <summary>カメラ移動に必要な三角関数のシータに相当する値X軸</summary>
    float _thetaX = 0;
    /// <summary>カメラ移動に必要な三角関数のシータに相当する値Y軸</summary>
    float _thetaY = 0;
    void Awake() => _input = GameObject.FindFirstObjectByType<ACInputHandler>();
    void Start()
    {
        //NULLだったら警告ログを吐き出す
        if (GetComponent<Camera>() == null) Debug.LogWarning("プレイヤーカメラが見つからない");
        if (_centerTransform == null) Debug.LogWarning("ターゲットの座標がnullだよ");
        //Update Name
        this.gameObject.tag = "MainCamera";
        _acMove = GameObject.FindAnyObjectByType<MechMovementComponent>();
        _occ = GetComponent<Occulutioner>();
    }
    void FixedUpdate()
    {
        GetLookInput();
        RotateSequence(_rotateRadius);
        TargettingSequence(_centerTransform);
        OcculusionSequence();
    }
    #region privateメソッド
    /// <summary>視点移動入力受け取り、格納処理</summary>
    void GetLookInput()
    {
        float inputX = _input.LookInput.x * _sencitivity.x * .01f;
        _thetaX += inputX;
        float inputY = _input.LookInput.y * _sencitivity.y * .01f;
        _thetaY += inputY;
    }
    /// <summary>オクルージョン処理</summary>
    void OcculusionSequence()
    {
        _occ.OcculusionSequence();
    }
    /// <summary>回転処理</summary>
    void RotateSequence(float rotateRadius)
    {
        if (_acMove.IsGrounded)//接地時
        {
            _thetaY = Mathf.Clamp(_thetaY, -_rollAngleAbsValue, _rollAngleAbsValue);
        }
        else if (_acMove.IsHovering)//滞空時
        {
            _thetaY = Mathf.Clamp(_thetaY, -_rollAngleAbsValue * 2, _rollAngleAbsValue * 2);
        }
        //回転の反転の符号の初期化
        var signX = (_inverseRotationX) ? -1 : 1;
        var signY = (_inverseRotationY) ? -1 : 1;
        this.transform.position =
            new Vector3
            (Mathf.Cos(_thetaX) * signX
            , Mathf.Sin(_thetaY) * signY
            , Mathf.Sin(_thetaX) * signX)
            * rotateRadius
            + _centerTransform.position;
    }
    /// <summary>プレイヤー捕捉処理</summary>
    void TargettingSequence(Transform followTransform)
    {
        //LookRotationの第一引数に正面方向のベクトルを指定してターゲットのオブジェクトを向く
        this.transform.rotation =
        Quaternion.LookRotation((followTransform.position - this.transform.position)
        , Vector3.up);
        //各方向ベクトル値初期化
        var f = this.transform.forward; f.y = 0;
        _forward = f;
        var r = this.transform.right; r.y = 0;
        _right = r;
    }
    #endregion
    #region publicメソッド
    #endregion
}