using UnityEngine;
public class LockOnTarget : MonoBehaviour
{
    GameObject _targetPoint;
    Vector2 _screenPosition;
    public Vector2 ScreenPosition => _screenPosition;
    bool _isTrasable = false;
    public bool IsCanLockOn => _isTrasable;
    void Start()
    {
        //�ߑ��p�̃I�u�W�F�N�g�̐���
        _targetPoint = new();
        _targetPoint.transform.parent = this.transform;
        _targetPoint.transform.localPosition = Vector3.zero;
    }
    void Update()
    {
        //�J�����̒��S����̋��������b�N�I���͈͓�������
        var cam = GameObject.FindAnyObjectByType<Camera>();
        var buff = cam.WorldToViewportPoint(_targetPoint.transform.position);
        Vector2 point = new(buff.x, buff.y);
        _screenPosition = point;
        float dx = (point.x - cam.rect.center.x);
        float dy = (point.y - cam.rect.center.y);
        float dx2 = dx * dx;
        float dy2 = dy * dy;
        _isTrasable = (dx2 < .05f) && (dy2 < .05f);
        if (_isTrasable)
        {
            var camMan = GameObject.FindAnyObjectByType<ACCAMManager>();
            camMan.AppendTargetToList(this.transform);
        }
    }
}