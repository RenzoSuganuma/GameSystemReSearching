using System.Collections.Generic;
using UnityEngine;
using RuntimeLog;
/// <summary>ACのカメラ動作コンポーネント</summary>
[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(Rigidbody))]
public class ACCAMComponentAlpha : MonoBehaviour
{
    /// <summary>ランタイムログ</summary>
    RuntimeLogComponent _logComponent;
    /// <summary>カメラの中心座標</summary>
    [SerializeField] Transform _centerTransform;
    /// <summary>カメラ位置のオフセット</summary>
    [SerializeField] Vector3 _offset;
    /// <summary>入力感度</summary>
    [SerializeField] Vector2 _sencitivity;
    /// <summary>カメラの高さ</summary>
    [SerializeField] float _camHeight;
    /// <summary>回転半径</summary>
    [SerializeField] float _rotateRadius;
    /// <summary>オクルージョンさせるのにアサインする透明の描写をするためのマテリアル</summary>
    [SerializeField] Material _assignTransparentMaterial;
    /// <summary>カメラ移動に必要な三角関数のシータに対応する値X軸</summary>
    float _thetaX = 0;
    /// <summary>カメラ移動に必要な三角関数のシータに対応する値Y軸</summary>
    float _thetaY = 0;
    /// <summary>入力ハンドラー</summary>
    ACInputHandler _inputHandler;
    void Start()
    {
        //ログコンポーネントのインスタンス化
        _logComponent = new(new Rect(0, 0, 100, 100));
        //入力インスタンス化
        _inputHandler = GameObject.FindFirstObjectByType<ACInputHandler>();
        //NULLだったら警告ログを吐き出す
        if (GetComponent<Camera>() == null) Debug.LogWarning("プレイヤーカメラが見つからない");
        if (_centerTransform == null) Debug.LogWarning("ターゲットの座標がnullだよ");
        if (GetComponent<Rigidbody>() == null) Debug.Log("剛体コンポーネントがない");
        //ターゲットを向く
        TargetingSequence(_centerTransform);
        //Rigidbodyプロパティ初期化 これがないと衝突の判定ができない
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        rigidbody.isKinematic = true;
        rigidbody.freezeRotation = true;
    }
    void Update()
    {
        //回転処理 横回転
        RotateSequenceX();
        //ターゲットを向く
        TargetingSequence(_centerTransform);
        //オクルージョン
        OcculusionPrepareSequence();
    }
    #region privateメソッド
    /// <summary>オクルージョンに必要なプロパティなどの準備</summary>
    private void OcculusionPrepareSequence()
    {
        //中心との距離を算出
        var dis = Vector3.Distance(_centerTransform.position, this.transform.position);
        BoxCollider collider = GetComponent<BoxCollider>();
        //各パラメーターの初期化
        collider.isTrigger = true;
        collider.providesContacts = true;
        collider.center = new Vector3(0, 0, dis / 2f);
        collider.size = new Vector3(collider.size.x, collider.size.y, dis);
    }
    /// <summary>オクルージョン処理</summary>
    private void OcculusionSequence(Renderer renderer, OcculutionMode mode)
    {
        OcculutionTarget occTarget;
        switch (mode)
        {
            //透明化処理が指定されてるとき
            case OcculutionMode.Transparent:
                {
                    //透明なもののマテリアル
                    renderer.material = _assignTransparentMaterial;
                    break;
                }
            //透明化解除処理が指定されてるとき
            case OcculutionMode.Normal:
                {
                    //オクルージョンターゲットにOccukutionTargetコンポーネントがアタッチされていればそれが保持する通常のマテリアルにする
                    renderer.material = 
                        (renderer.gameObject.TryGetComponent<OcculutionTarget>(out occTarget))
                        ? occTarget.TargetMaterial : null;
                    break;
                }
        }
    }
    /// <summary>Y軸回転処理</summary>
    private void RotateSequenceX()
    {
        //入力処理
        float inputX = _inputHandler.LookInput.x * _sencitivity.x * .01f;
        _thetaX += inputX;
        float inputY = _inputHandler.LookInput.y * _sencitivity.y * .01f;
        _thetaY += inputY;
        //ワールドZ軸→sin() ワールドX軸→cos()
        //三角関数を使って円の軌跡をたどらせる。各軸の成分にオフセットも掛ける
        this.transform.position =
            new Vector3(Mathf.Sin(_thetaX) + _centerTransform.position.x + _offset.x
            , _centerTransform.position.y + (_camHeight * .1f) + _offset.y
            , Mathf.Cos(_thetaX) + _centerTransform.position.z + _offset.z) * _rotateRadius;
    }
    /// <summary>捕捉処理</summary>
    private void TargetingSequence(Transform targetTransform)
    {
        //LookRotationの第一引数に正面方向のベクトルを指定してターゲットのオブジェクトを向く
        this.transform.rotation =
            Quaternion.LookRotation(targetTransform.position - this.transform.position
            , Vector3.up);
    }
    #endregion
    private void OnGUI()
    {
        //ログの描写表示
        _logComponent.DisplayLog("ログ出力テスト");
    }
    #region 衝突判定
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
        {
            OcculusionSequence(other.GetComponent<Renderer>(), OcculutionMode.Transparent);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player"))
        {
            OcculusionSequence(other.GetComponent<Renderer>(), OcculutionMode.Normal);
        }
    }
    #endregion
    private void OnDrawGizmos()
    {
        //回転半径の球メッシュ描写
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(_centerTransform.position, _rotateRadius);
    }
}
/// <summary>オクルージョンモード</summary>
public enum OcculutionMode
{
    Normal,//もとに戻すときにこれを指定
    Transparent//透明にするときにこれを指定
}