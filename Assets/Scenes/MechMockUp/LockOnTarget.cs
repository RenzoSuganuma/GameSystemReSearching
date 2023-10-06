using Cinemachine.Utility;
using UnityEngine;
public class LockOnTarget : MonoBehaviour
{
    GameObject _targetPoint;
    bool _isTrasable = false;
    public bool IsCanLockOn => _isTrasable;
    private void Start()
    {
        //捕捉用のオブジェクトの生成
        _targetPoint = new();
        _targetPoint.transform.parent = this.transform;
        _targetPoint.transform.localPosition = Vector3.zero;
    }
    private void Update()
    {
        //カメラの中心からの距離がロックオン範囲内か判定
        var cam = GameObject.FindAnyObjectByType<Camera>();
        var buff = cam.WorldToViewportPoint(_targetPoint.transform.position);
        Vector2 point = new(buff.x, buff.y);
        float dx = (point.x - cam.rect.center.x);
        float dy = (point.y - cam.rect.center.y);
        float dx2 = dx * dx;
        float dy2 = dy * dy;
        _isTrasable = (dx2 < .05f) && (dy2 < .05f);
    }
}