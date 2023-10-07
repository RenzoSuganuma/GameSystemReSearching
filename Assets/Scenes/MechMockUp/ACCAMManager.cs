using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DGW;
/// <summary>�J�������[�h</summary>
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
    CustomMethods _customMethods;
    List<Transform> _assistTargets = new();
    bool _isAimAssist = false;
    CameraMode _mode = CameraMode.Normal;
    public CameraMode CamMode => _mode;
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
        _aimAssistCAM.ApplyAimTarget(_assistTargets[0]);
    }
    private void Start()
    {
        _orbitCAM = GameObject.FindAnyObjectByType<OrbitalCameraComponent>();
        _aimAssistCAM = GameObject.FindAnyObjectByType<AimAssistCameraComponent>();
        _customMethods = new();
        SetCameraMode(CameraMode.Normal);
    }
    private void Update()
    {
        _customMethods.When(_input.LookInput.magnitude > 0 && _mode == CameraMode.AimAssist
            , () =>
            {
                _isAimAssist = false;
                SwitchCameraMode();
            });
        _customMethods.When(_isAimAssist, () =>
        {
            ApplyTargetToAssistCam();
        });
    }
    /// <summary>�ߑ��Ώۃ��X�g�ɓo�^���郁�\�b�h</summary>
    /// <param name="target"></param>
    public void AppendTargetToList(Transform target)
    {
        _customMethods.When(!_assistTargets.Contains(target), () =>
        {
            _assistTargets.Add(target);
        });
    }
    /// <summary>�J�������[�h�ؑ֓��͂����ꂽ��Ăяo�����</summary>
    void SwitchCameraMode()
    {
        _mode = (_isAimAssist) ? CameraMode.AimAssist : CameraMode.Normal;
        SetCameraMode(_mode);//�J�������[�h�؂�ւ�
    }
    /// <summary>�J���������[�h�̕ϐ��l�ɉ����Đ؂�ւ��郁�\�b�h</summary>
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
            default:
                {
                    break;
                }
        }
    }
}