using System.Collections.Generic;
using UnityEngine;
using DebugLogRecorder;
/// <summary>ACのカメラ動作コンポーネント</summary>
public class ACCAMComponent : MonoBehaviour
{
    /// <summary>入力ハンドラー</summary>
    ACInputHandler _input;
    /// <summary>オクルージョンしたオブジェクトを格納しておく</summary>
    GameObject _occuludedObject;
    /// <summary>カメラのオブジェクト</summary>
    GameObject _parentObject;
    /// <summary>ロックオン可能なオブジェクトを格納しておく</summary>
    List<Transform> _lockOnTargetTransform = new();
    public List<Transform> LockOnTargetList => _lockOnTargetTransform;
    /// <summary>ランライムログ</summary>
    RuntimeLogComponent _log;
    /// <summary>プレイヤー</summary>
    ACMovementComponent _acMove;
    /// <summary>正面の方向のベクトル</summary>
    Vector3 _direction;
    /// <summary>正面の方向のベクトル(readonly)</summary>
    public Vector3 Forward => _direction;
    /// <summary>カメラの中心座標</summary>
    [SerializeField] Transform _centerTransform;
    /// <summary>カメラオフセット</summary>
    [SerializeField] Vector3 _offset;
    /// <summary>カメラオフセット</summary>
    [SerializeField] Vector3 _lookOffset;
    /// <summary>入力感度</summary>
    [SerializeField] Vector2 _sencitivity = new(1, .5f);
    /// <summary>回転半径</summary>
    [SerializeField] float _rotateRadius;
    /// <summary>回転半径</summary>
    [SerializeField] float _targettingLimitDistance;
    public float TargettingLimitDistance => _targettingLimitDistance;
    /// <summary>X軸回転角度のクランプするときの値の絶対値</summary>
    [SerializeField, Range(.1f, .5f)] float _rollAngleAbsValue = .3f;
    /// <summary>回転の反転を有効にするかのフラグ</summary>
    [SerializeField] bool _inverseRotationY;
    /// <summary>回転の反転を有効にするかのフラグ</summary>
    [SerializeField] bool _inverseRotationX;
    /// <summary>オクルージョンさせるのにアサインする透明の描写をするためのマテリアル</summary>
    [SerializeField] Material _transparentMat;
    /// <summary>カメラ移動に必要な三角関数のシータに対応する値X軸</summary>
    float _thetaX = 0;
    /// <summary>カメラ移動に必要な三角関数のシータに対応する値Y軸</summary>
    float _thetaY = 0;
    /// <summary>照準アシストするかのフラグ</summary>
    bool _isTargetAssisting = false;
    public bool IsTargetAssisting => _isTargetAssisting;
    private void Awake()
    {
        _input = GameObject.FindFirstObjectByType<ACInputHandler>();
    }
    private void OnEnable()
    {
        _input.LockOnAssist += StartTargetAssist;
    }
    private void OnDisable()
    {
        _input.LockOnAssist -= StartTargetAssist;
    }
    void Start()
    {
        //NULLだったら警告ログを吐き出す
        if (GetComponent<Camera>() == null) Debug.LogWarning("プレイヤーカメラが見つからない");
        if (_centerTransform == null) Debug.LogWarning("ターゲットの座標がnullだよ");
        this.gameObject.tag = "MainCamera";
        _acMove = GameObject.FindFirstObjectByType<ACMovementComponent>();
        _log = new(new Rect(0, 500, 300, 300));
        _parentObject = new GameObject("CameraPositionReference");
        this.transform.parent = _parentObject.transform;
        this.transform.localPosition = Vector3.zero;
        ACLookSequence(_centerTransform);
    }
    void Update()
    {
        RotateSequence(_isTargetAssisting, (_isTargetAssisting) ? _rotateRadius / 2 : _rotateRadius);
        ACLookSequence(_centerTransform);
        OcculusionSequence();
        var lockOnPermitState = (_lockOnTargetTransform != null && _lockOnTargetTransform.Count > 0)
            ? _lockOnTargetTransform[0].GetComponent<LockOnTarget>().IsCanLockOn : false;
        LockOnSequence(_isTargetAssisting, lockOnPermitState);
    }
    #region privateメソッド
    /// <summary>オクルージョン処理</summary>
    private void OcculusionSequence()
    {
        //ターゲットとの距離の算出
        var dis = Vector3.Distance(_centerTransform.position, this.transform.position);
        //ターゲットに向かう向きのベクトルの算出
        var dir = _centerTransform.position - this.transform.position;
        //光線の生成
        Ray ray = new(this.transform.position, dir);
        RaycastHit hit;
        Debug.DrawRay(ray.origin, ray.direction, Color.magenta, dis);
        //光線が何かに当たったら
        if (Physics.Raycast(ray, out hit))
        {
            //オクルージョン処理
            if (hit.transform.gameObject.TryGetComponent<OcculutionTarget>(out OcculutionTarget target))
            {
                target.OverwriteMaterial(_transparentMat);
                _occuludedObject = target.gameObject;
            }
            //オクルージョン解除処理
            else if (_occuludedObject != null)
            {
                if (_occuludedObject.TryGetComponent<OcculutionTarget>(out OcculutionTarget component))
                {
                    component.OverwriteMaterial(component.Material);
                }
            }
        }
    }
    /// <summary>Y軸回転処理</summary>
    private void RotateSequence(bool isTargetAssisting, float rotateRadius)
    {
        if (!isTargetAssisting)
        {
            float inputX = _input.LookInput.x * _sencitivity.x * .01f;
            _thetaX += inputX;
            float inputY = _input.LookInput.y * _sencitivity.y * .01f;
            _thetaY += inputY;
        }
        //X軸回転に使う引数の値のクランプ
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
        //座標更新
        if (!_isTargetAssisting)
        {
            _parentObject.transform.position =
                new Vector3(Mathf.Cos(_thetaX) * signX
                , Mathf.Sin(_thetaY) * signY
                , Mathf.Sin(_thetaX) * signX)
                * rotateRadius
                + _centerTransform.position;
            this.transform.localPosition =
                  (transform.forward * _offset.z)
                + (transform.right * _offset.x)
                + (transform.up * _offset.y);
        }
        else
        {
            _parentObject.transform.position = _centerTransform.position + (_centerTransform.forward * -_rotateRadius) + (_centerTransform.up * 5);
        }
    }
    private void StartTargetAssist()
    {
        if (_lockOnTargetTransform[0] != null)
        {
            _isTargetAssisting = !_isTargetAssisting;
        }
    }
    /// <summary>捕捉処理</summary>
    private void ACLookSequence(Transform followTransform)
    {
        //LookRotationの第一引数に正面方向のベクトルを指定してターゲットのオブジェクトを向く
        this.transform.rotation =
        Quaternion.LookRotation((followTransform.position - this.transform.position) + _lookOffset + _offset
        , Vector3.up);
        //正面ベクトルの初期化
        _direction = new(this.transform.forward.x, 0, this.transform.forward.z);
    }
    private void LockOnSequence(bool isAssistingAim, bool isCanLockOn)
    {
        if (isAssistingAim && _lockOnTargetTransform[0] != null && isCanLockOn)
        {
            //LookRotationの第一引数に正面方向のベクトルを指定してターゲットのオブジェクトを向く
            this.transform.rotation =
                Quaternion.LookRotation((_lockOnTargetTransform[0].position - this.transform.position) + _lookOffset + _offset
                , Vector3.up);
            //正面ベクトルの初期化
            _direction = new(this.transform.forward.x, 0, this.transform.forward.z);
            if (Vector3.Distance(_lockOnTargetTransform[0].position, this.transform.position) > _targettingLimitDistance)
            {
                _isTargetAssisting = false;
            }
        }
    }
    #endregion
    #region publicメソッド
    public void AddLockOnTargetToList(Transform target)
    {
        _lockOnTargetTransform.Add(target);
    }
    public void RemoveLockOnTargetToList(Transform target)
    {
        _lockOnTargetTransform.Remove(target);
    }
    #endregion
    private void OnDrawGizmos()
    {
        //回転半径の球メッシュ描写
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(_centerTransform.position, _rotateRadius);
    }
}