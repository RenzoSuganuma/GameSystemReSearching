using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class LockOnTarget : MonoBehaviour
{
    GameObject _targetPoint;
    bool _isCanLockOn = false;
    public bool IsCanLockOn => _isCanLockOn;
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
        var buff = Camera.main.WorldToViewportPoint(_targetPoint.transform.position);
        Vector2 point = new(buff.x, buff.y);
        float dx = (point.x - Camera.main.rect.center.x);
        float dy = (point.y - Camera.main.rect.center.y);
        float dx2 = dx * dx;
        float dy2 = dy * dy;
        _isCanLockOn = (dx2 < .05f) && (dy2 < .05f);
        if (_isCanLockOn)
        {
            var component = GameObject.FindAnyObjectByType<ACCAMComponent>();
            if (!component.LockOnTargetList.Contains(this.transform))
            {
                component.AddLockOnTargetToList(this.transform);
            }
        }
        else
        {

            var component = GameObject.FindAnyObjectByType<ACCAMComponent>();
            if (component.LockOnTargetList.Contains(this.transform))
            {
                component.RemoveLockOnTargetToList(this.transform);
            }
        }
        Debug.Log($"dx2:{dx2},dy2:{dy2}");
    }
}