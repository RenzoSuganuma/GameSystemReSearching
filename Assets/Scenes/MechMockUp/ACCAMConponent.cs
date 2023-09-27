using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
public class ACCAMComponentAlpha : MonoBehaviour
{
    [SerializeField] Transform _target;
    [SerializeField] Vector3 _offset;
    [SerializeField] Vector2 _sencitivity;
    [SerializeField] float _camHeight;
    [SerializeField] float _rotateRadius;
    [SerializeField] float _camLockOnDistance;
    float _thetaX = 0;
    float _thetaY = 0;
    void Start()
    {
        //NULLだったら何もしない
        if (GetComponent<Camera>() == null) return;
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
    private void OnTriggerEnter(Collider other)
    {
    }
    private void OnTriggerExit(Collider other)
    {
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(this.transform.position, _camLockOnDistance);
    }
}
