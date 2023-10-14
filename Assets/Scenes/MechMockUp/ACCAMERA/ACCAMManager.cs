using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DGW;
using static DGW.OriginalMethods;
/// <summary>カメラモード</summary>
public enum CameraMode
{
    Normal,
    AimAssist
}
/// <summary>補足対象の情報構造体</summary>
public struct FocusableObject
{
    public Transform Transform;
    public Vector2 ScreenPosition;
    public FocusableObject(Transform transform, Vector2 screenPos)
    {
        this.Transform = transform;
        this.ScreenPosition = screenPos;
    }
}
public class ACCAMManager : MonoBehaviour
{
    ACInputHandler _input;
    OrbitalCameraComponent _orbitCAM;
    AimAssistCameraComponent _aimAssistCAM;
    List<FocusableObject> _assistTargets = new();
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
        if (Mathf.Abs(_horizontalInput) > 50)
        {
            if (_horizontalInput > 0 && _assistTargets.Count > _targetIndex + 1)
            {
                _targetIndex++;
            }
            else if (_horizontalInput < 0 && _targetIndex - 1 > -1)
            {
                _targetIndex--;
            }
            _horizontalInput = 0;
        }
        _aimAssistCAM.ApplyAimTarget(_assistTargets[_targetIndex].Transform);
    }
    private void Start()
    {
        _orbitCAM = GameObject.FindAnyObjectByType<OrbitalCameraComponent>();
        _aimAssistCAM = GameObject.FindAnyObjectByType<AimAssistCameraComponent>();
        SetCameraMode(CameraMode.Normal);
    }
    private void Update()
    {
        OneShot(_input.LookInput.magnitude > 0 && _mode == CameraMode.AimAssist, () =>
        {
            _isAimAssist = false;
            SwitchCameraMode();
        });

        OneShot(_isAimAssist, () =>
        {
            ApplyTargetToAssistCam();
        });
    }
    /// <summary>捕捉対象リストに登録するメソッド</summary>
    /// <param name="target"></param>
    public void AppendTargetToList(FocusableObject target)
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