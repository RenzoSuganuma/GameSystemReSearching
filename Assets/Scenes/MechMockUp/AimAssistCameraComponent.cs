using DGW;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public class AimAssistCameraComponent : MonoBehaviour
{
    Occulutioner _occ;
    Vector3 _forward;
    public Vector3 Forward => _forward;
    Vector3 _right;
    public Vector3 Right => _right;
    [SerializeField] Transform _aimTargetTransform;
    [SerializeField] Transform _centerTransform;
    [SerializeField] Vector3 _offset;
    private void Start()
    {
        _occ = GameObject.FindAnyObjectByType<Occulutioner>();
    }
    private void Update()
    {
        OcculusionSequence();
        TargettingSequence(_aimTargetTransform);
        UpdateCoordinateSequence();
    }
    /// <summary>オクルージョン処理</summary>
    private void OcculusionSequence()
    {
        _occ.OcculusionSequence();
    }
    /// <summary>プレイヤー捕捉処理</summary>
    private void TargettingSequence(Transform followTransform)
    {
        //LookRotationの第一引数に正面方向のベクトルを指定してターゲットのオブジェクトを向く
        this.transform.rotation =
        Quaternion.LookRotation((followTransform.position - this.transform.position)
        , Vector3.up);
        //各方向ベクトル値初期化
        var f = this.transform.forward; f.y = 0;
        _forward = f;
        var r = this.transform.right; r.y = 0;
        _right = r;

    }
    private void UpdateCoordinateSequence()
    {
        this.transform.position = _centerTransform.position + _offset;
    }
}
