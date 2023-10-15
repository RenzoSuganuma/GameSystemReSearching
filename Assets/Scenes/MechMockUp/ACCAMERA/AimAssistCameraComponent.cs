using DGW;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class AimAssistCameraComponent : MonoBehaviour
{
    Occulutioner _occ;
    ACInputHandler _input;
    List<Transform> _targetsList = new();
    Vector3 _forward;
    public Vector3 Forward => _forward;
    Vector3 _right;
    public Vector3 Right => _right;
    Transform _aimTargetTransform;
    [SerializeField] Transform _centerTransform;
    [SerializeField] Vector3 _offset;
    int _targetIndex = 0;
    float _horizontalInput = 0;
    bool _wasTargetChanged = false;
    void Start()
    {
        _occ = GameObject.FindAnyObjectByType<Occulutioner>();
        _input = GameObject.FindAnyObjectByType<ACInputHandler>();
    }
    void FixedUpdate()
    {
        ApplyTargetToAssistCam(_targetsList);
        LookingSequence(_aimTargetTransform);
        UpdateCoordinateSequence();
        OcculusionSequence();
    }
    void ApplyTargetToAssistCam(List<Transform> targets)
    {
        if (targets == null) { return; }
        ApplyAimTarget(targets[_targetIndex].transform);
        _horizontalInput += _input.LookInput.x;//入力値受けとり
        if (Mathf.Abs(_horizontalInput) > 0 && !_wasTargetChanged)
        {//左右入力に応じてターゲット更新
            if (_horizontalInput > 0)
            {
                var currentTarget = targets[_targetIndex];
                for (int i = 0; i < targets.Count; i++)
                {
                    if (currentTarget.GetComponent<LockOnTarget>().ScreenPosition.x
                        < targets[i].GetComponent<LockOnTarget>().ScreenPosition.x)
                    {
                        _targetIndex = i;
                        _wasTargetChanged = true;
                    }
                    _horizontalInput = 0;
                }
                ApplyAimTarget(targets[_targetIndex].transform);
            }
            else if (_horizontalInput < 0)
            {
                var currentTarget = targets[_targetIndex];
                for (int i = 0; i < targets.Count; i++)
                {
                    if (currentTarget.GetComponent<LockOnTarget>().ScreenPosition.x
                        > targets[i].GetComponent<LockOnTarget>().ScreenPosition.x)
                    {
                        _targetIndex = i;
                        _wasTargetChanged = true;
                    }
                    _horizontalInput = 0;
                }
                ApplyAimTarget(targets[_targetIndex].transform);
            }
        }
        else if (_horizontalInput != 0 && _wasTargetChanged && _input.LookInput.x == 0)
        {
            _horizontalInput = 0;
            _wasTargetChanged = false;
        }
    }
    /// <summary>オクルージョン処理</summary>
    void OcculusionSequence()
    {
        _occ.OcculusionSequence();
    }
    /// <summary>プレイヤー捕捉処理</summary>
    void LookingSequence(Transform followTransform)
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
    void UpdateCoordinateSequence()
    {
        this.transform.position = _centerTransform.position
            + _centerTransform.forward * (_offset.z)
            + _centerTransform.right * (_offset.x)
            + _centerTransform.up * (_offset.y);
    }
    void ApplyAimTarget(Transform target)
    {
        this._aimTargetTransform = target;
    }
    public void UpdateAimTarget(List<Transform> targets) => this._targetsList = targets;
}