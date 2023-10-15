using System.Collections.Generic;
using UnityEngine;
using static DGW.OriginalMethods;
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
    /// <summary> �G�C���A�V�X�g�^�[�Q�b�g </summary>
    List<Transform> _assistTargets = new();
    bool _isAimAssist = false;
    /// <summary> �ʏ킩�G�C���A�V�X�g�����s�����̃t���O </summary>
    CameraMode _mode = CameraMode.Normal;
    public CameraMode CamMode => _mode;
    void Awake() => _input = GameObject.FindAnyObjectByType<ACInputHandler>();
    void OnEnable()
    {
        _input.LockOnAssist += OnAimAssist;
        _input.LockOnAssist += SwitchCameraMode;
    }
    void OnDisable()
    {
        _input.LockOnAssist -= OnAimAssist;
        _input.LockOnAssist -= SwitchCameraMode;
    }
    void OnAimAssist()
    {
        _isAimAssist = !_isAimAssist;
    }
    void Start()
    {
        _orbitCAM = GameObject.FindAnyObjectByType<OrbitalCameraComponent>();
        _aimAssistCAM = GameObject.FindAnyObjectByType<AimAssistCameraComponent>();
        SetCameraMode(CameraMode.Normal);
    }
    void FixedUpdate()
    {
        DOnce(_isAimAssist && _mode == CameraMode.AimAssist, () =>
        {
            _aimAssistCAM.UpdateAimTarget(_assistTargets);
        });
    }
    /// <summary>�ߑ��Ώۃ��X�g�ɓo�^���郁�\�b�h</summary>
    /// <param name="target"></param>
    public void AppendTargetToList(Transform target)
    {
        DOnce(!_assistTargets.Contains(target), () =>
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
        }
    }
}