using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DGW;
using static DGW.OriginalMethods;
using System.Linq;
/// <summary>カメラモード</summary>
public enum CameraMode
{
    Normal,
    AimAssist
}
public class ACCAMManager : MonoBehaviour
{
    ACInputHandler _input;
    OrbitalCameraComponent _orbitCAM;
    AimAssistCameraComponent _aimAssistCAM;
    List<Transform> _assistTargets = new();
    bool _isAimAssist = false;
    CameraMode _mode = CameraMode.Normal;
    public CameraMode CamMode => _mode;
    //temporary
    int _targetIndex = 0;
    float _horizontalInput = 0;
    private void Awake()
    {
        _input = GameObject.FindAnyObjectByType<ACInputHandler>();
    }
    private void OnEnable()
    {
        _input.LockOnAssist += OnAimAssist;
        _input.LockOnAssist += SwitchCameraMode;
    }
    private void OnDisable()
    {
        _input.LockOnAssist -= OnAimAssist;
        _input.LockOnAssist -= SwitchCameraMode;
    }
    void OnAimAssist()
    {
        _isAimAssist = !_isAimAssist;
    }
    void ApplyTargetToAssistCam()
    {
        _horizontalInput += _input.LookInput.x;//入力値受けとり
        if (Mathf.Abs(_horizontalInput) > 60)
        {
            if (_horizontalInput > 0)
            {
                _targetIndex++;
                if (_targetIndex > _assistTargets.Count - 1) _targetIndex = _assistTargets.Count - 1;
                _horizontalInput = 0;
                _aimAssistCAM.ApplyAimTarget(_assistTargets[_targetIndex].transform);
            }
            else if (_horizontalInput < 0)
            {
                _targetIndex--;
                if (_targetIndex <= 0) _targetIndex = 0;
                _horizontalInput = 0;
                _aimAssistCAM.ApplyAimTarget(_assistTargets[_targetIndex].transform);
            }
        }
        else if (_horizontalInput == 0)
        {
            _aimAssistCAM.ApplyAimTarget(_assistTargets[0].transform);
        }
    }
    private void Start()
    {
        _orbitCAM = GameObject.FindAnyObjectByType<OrbitalCameraComponent>();
        _aimAssistCAM = GameObject.FindAnyObjectByType<AimAssistCameraComponent>();
        SetCameraMode(CameraMode.Normal);
    }
    private void Update()
    {
        OneShot(_isAimAssist, () =>
        {
            ApplyTargetToAssistCam();
        });
    }
    /// <summary>捕捉対象リストに登録するメソッド</summary>
    /// <param name="target"></param>
    public void AppendTargetToList(Transform target)
    {
        OneShot(!_assistTargets.Contains(target), () =>
        {
            _assistTargets.Add(target);
        });
    }
    /// <summary>カメラモード切替入力がされたら呼び出される</summary>
    void SwitchCameraMode()
    {
        _mode = (_isAimAssist) ? CameraMode.AimAssist : CameraMode.Normal;
        SetCameraMode(_mode);//カメラモード切り替え
    }
    /// <summary>カメラをモードの変数値に応じて切り替えるメソッド</summary>
    /// <param name="mode"></param>
    public void SetCameraMode(CameraMode mode)
    {
        switch (mode)
        {
            case CameraMode.Normal:
                {
                    _orbitCAM.gameObject.SetActive(true);
                    _aimAssistCAM.gameObject.SetActive(false);
                    break;
                }
            case CameraMode.AimAssist:
                {
                    _orbitCAM.gameObject.SetActive(false);
                    _aimAssistCAM.gameObject.SetActive(true);
                    break;
                }
        }
    }
}