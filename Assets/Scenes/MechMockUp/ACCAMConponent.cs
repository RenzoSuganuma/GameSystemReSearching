using System.Collections.Generic;
using UnityEngine;
using RuntimeLog;
[RequireComponent(typeof(SphereCollider))]
public class ACCAMComponentAlpha : MonoBehaviour
{
    [SerializeField] Transform _target;
    [SerializeField] Vector3 _offset;
    [SerializeField] Vector2 _sencitivity;
    [SerializeField] float _camHeight;
    [SerializeField] float _rotateRadius;
    [SerializeField] float _camLockOnDistance;
    SphereCollider _sphereCollider;
    RuntimeLogComponent _logComponent;
    float _thetaX = 0;
    float _thetaY = 0;
    void Start()
    {
        //ログコンポーネントのインスタンス化
        _logComponent = new(new Rect(0, 0, 100, 100));
        //NULLだったら警告ログを吐き出す
        if (GetComponent<Camera>() == null) Debug.LogWarning("プレイヤーカメラが見つからない");
        if (!TryGetComponent<SphereCollider>(out _sphereCollider)) Debug.LogWarning("球コライダーが見つからない");
        //コンポーネント値初期化(球コライダー)
        _sphereCollider.isTrigger = true;
        _sphereCollider.providesContacts = true;
        _sphereCollider.radius = _camLockOnDistance;
        //ターゲットを向く
        this.transform.rotation =
            Quaternion.LookRotation(_target.transform.position - this.transform.position
            , Vector3.up);
    }
    void Update()
    {
        float inputX = Input.GetAxis("Mouse X") * _sencitivity.x * .01f;
        _thetaX += inputX;
        float inputY = Input.GetAxis("Mouse Y") * _sencitivity.y * .01f;
        _thetaY += inputY;
        //ワールドZ軸→sin() ワールドX軸→cos()
        //回転処理 横回転
        this.transform.position =
            new Vector3(Mathf.Sin(_thetaX) + _target.position.x
            , _target.position.y + (_camHeight * .1f)
            , Mathf.Cos(_thetaX) + _target.position.z) * _rotateRadius;
        //ターゲットを向く
        this.transform.rotation =
        Quaternion.LookRotation((_target.transform.position + _offset) - this.transform.position
        , Vector3.up);
        //ロックオン範囲内のオブジェクトを検索
    }
    private void OnGUI()
    {
        //ログの描写表示
        _logComponent.DisplayLog("ログ出力テスト");
    }
    private void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("Player"))
            other.gameObject.SetActive(false);
    }
    private void OnTriggerExit(Collider other)
    {
    }
    private void OnDrawGizmos()
    {
        //補足距離の球メッシュ描写
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(this.transform.position, _camLockOnDistance);
    }
}
