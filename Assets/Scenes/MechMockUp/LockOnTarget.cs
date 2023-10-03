using UnityEngine;
public class LockOnTarget : MonoBehaviour
{
    GameObject _targetPoint;
    bool _isCanLockOn = false;
    public bool IsCanLockOn => _isCanLockOn;
    private void Start()
    {
        //�ߑ��p�̃I�u�W�F�N�g�̐���
        _targetPoint = new();
        _targetPoint.transform.parent = this.transform;
        _targetPoint.transform.localPosition = Vector3.zero;
    }
    private void Update()
    {
        //�J�����̒��S����̋��������b�N�I���͈͓�������
        var buff = Camera.main.WorldToViewportPoint(_targetPoint.transform.position);
        var cam = GameObject.FindAnyObjectByType<ACCAMComponent>();
        Vector2 point = new(buff.x, buff.y);
        float dx = (point.x - Camera.main.rect.center.x);
        float dy = (point.y - Camera.main.rect.center.y);
        float dx2 = dx * dx;
        float dy2 = dy * dy;
        _isCanLockOn = (dx2 < .05f) && (dy2 < .05f)
            && Vector3.Distance(cam.transform.position, this.transform.position) < cam.TargettingLimitDistance;
        if (_isCanLockOn)
        {
            if (!cam.LockOnTargetList.Contains(this.transform))
            {
                cam.AddLockOnTargetToList(this.transform);
            }
        }
        else
        {
            if (cam.LockOnTargetList.Contains(this.transform))
            {
                cam.RemoveLockOnTargetToList(this.transform);
            }
        }
        Debug.Log($"dx2:{dx2},dy2:{dy2}");
    }
}