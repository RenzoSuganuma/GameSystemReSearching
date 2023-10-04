using UnityEngine;
public class LockOnTarget : MonoBehaviour
{
    GameObject _targetPoint;
    ACCAMComponent _cam;
    bool _isCanLockOn = false;
    public bool IsCanLockOn => _isCanLockOn;
    private void Awake()
    {
        _cam = GameObject.FindAnyObjectByType<ACCAMComponent>();
    }
    private void OnEnable()
    {
        _cam.LockOnDeniedEvent += () => { _cam.RemoveLockOnTargetToList(this.transform); };
    }
    private void OnDisable()
    {
        _cam.LockOnDeniedEvent -= () =>
        {
            if (_cam.LockOnTargetList.Contains(this.transform))
                _cam.RemoveLockOnTargetToList(this.transform);
        };
    }
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
        _isCanLockOn = (dx2 < .05f) && (dy2 < .05f)
            && Vector3.Distance(_cam.transform.position, this.transform.position) < _cam.TargettingLimitDistance;
        if (_isCanLockOn)
        {
            if (!_cam.LockOnTargetList.Contains(this.transform))
            {
                _cam.AddLockOnTargetToList(this.transform);
            }
        }
    }
}