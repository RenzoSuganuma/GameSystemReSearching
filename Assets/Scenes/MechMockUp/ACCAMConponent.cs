using System.Collections.Generic;
using UnityEngine;
using RuntimeLog;
[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(Rigidbody))]
public class ACCAMComponentAlpha : MonoBehaviour
{
    RuntimeLogComponent _logComponent;
    [SerializeField] Transform _target;
    [SerializeField] Vector3 _offset;
    [SerializeField] Vector2 _sencitivity;
    [SerializeField] float _camHeight;
    [SerializeField] float _rotateRadius;
    [SerializeField] float _camLockOnDistance;
    [SerializeField] Material _assignTransparentMaterial;
    float _thetaX = 0;
    float _thetaY = 0;
    void Start()
    {
        //ログコンポーネントのインスタンス化
        _logComponent = new(new Rect(0, 0, 100, 100));
        //NULLだったら警告ログを吐き出す
        if (GetComponent<Camera>() == null) Debug.LogWarning("プレイヤーカメラが見つからない");
        if (_target == null) Debug.LogWarning("ターゲットの座標がnullだよ");
        if (GetComponent<Rigidbody>() == null) Debug.Log("剛体コンポーネントがない");
        //ターゲットを向く
        TargetingSequence();
        //Rigidbodyプロパティ初期化 これがないと衝突の判定ができない
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        rigidbody.isKinematic = true;
    }
    void Update()
    {
        float inputX = Input.GetAxis("Mouse X") * _sencitivity.x * .01f;
        _thetaX += inputX;
        float inputY = Input.GetAxis("Mouse Y") * _sencitivity.y * .01f;
        _thetaY += inputY;
        //ワールドZ軸→sin() ワールドX軸→cos()
        //回転処理 横回転
        RotateSequenceX();
        //ターゲットを向く
        TargetingSequence();
        //オクルージョン
        OcculusionPrepareSequence();
    }
    private void OcculusionPrepareSequence()
    {
        var dis = Vector3.Distance(_target.position, this.transform.position);
        BoxCollider collider = GetComponent<BoxCollider>();
        collider.isTrigger = true;
        collider.providesContacts = true;
        collider.center = new Vector3(0, 0, dis / 2f);
        collider.size = new Vector3(collider.size.x, collider.size.y, dis);
    }
    private void OcculusionSequence(Renderer renderer, OcculutionMode mode)
    {
        switch (mode)
        {
            case OcculutionMode.Transparent:
                {
                    renderer.material = _assignTransparentMaterial;
                    break;
                }
            case OcculutionMode.Normal:
                {
                    renderer.material = (renderer.gameObject.TryGetComponent<OcculutionTarget>(out OcculutionTarget oTarget)) ? oTarget.TargetMaterial : null;
                    break;
                }
        }
    }
    private void RotateSequenceX()
    {
        this.transform.position =
            new Vector3(Mathf.Sin(_thetaX) + _target.position.x + _offset.x
            , _target.position.y + (_camHeight * .1f) + _offset.y
            , Mathf.Cos(_thetaX) + _target.position.z + _offset.z) * _rotateRadius;
    }
    private void TargetingSequence()
    {
        this.transform.rotation =
            Quaternion.LookRotation(_target.transform.position - this.transform.position
            , Vector3.up);
    }
    private void OnGUI()
    {
        //ログの描写表示
        _logComponent.DisplayLog("ログ出力テスト");
    }
    private void OnTriggerStay(Collider other)
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
    private void OnDrawGizmos()
    {
        //回転半径の球メッシュ描写
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(_target.position, _rotateRadius);
    }
}
public enum OcculutionMode
{
    Normal,
    Transparent
}