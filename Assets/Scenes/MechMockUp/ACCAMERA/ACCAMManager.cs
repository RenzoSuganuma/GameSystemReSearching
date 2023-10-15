using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DGW;
using static DGW.OriginalMethods;
using System.Linq;
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
    List<Transform> _assistTargets = new();
    bool _isAimAssist = false;
    CameraMode _mode = CameraMode.Normal;
    public CameraMode CamMode => _mode;
    //temporary
    int _targetIndex = 0;
    float _targetChangeInputTimeCount = 0;
    float _horizontalInput = 0;
    bool _targetChanged = false;
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
        if(_assistTargets == null) { return; }
        _aimAssistCAM.ApplyAimTarget(_assistTargets[_targetIndex].transform);
        _horizontalInput += _input.LookInput.x;//���͒l�󂯂Ƃ�
        if (Mathf.Abs(_horizontalInput) > 0 && !_targetChanged)
        {//���E���͂ɉ����ă^�[�Q�b�g�X�V
            if (_horizontalInput > 0)
            {
                var currentTarget = _assistTargets[_targetIndex];
                for (int i = 0; i < _assistTargets.Count; i++)
                {
                    if (currentTarget.GetComponent<LockOnTarget>().ScreenPosition.x
                        < _assistTargets[i].GetComponent<LockOnTarget>().ScreenPosition.x)
                    {
                        _aimAssistCAM.ApplyAimTarget(_assistTargets[i].transform);
                        _targetIndex = i;
                        _targetChanged = true;
                    }
                    _horizontalInput = 0;
                }
            }
            else if (_horizontalInput < 0)
            {
                var currentTarget = _assistTargets[_targetIndex];
                for (int i = 0; i < _assistTargets.Count; i++)
                {
                    if (currentTarget.GetComponent<LockOnTarget>().ScreenPosition.x
                        > _assistTargets[i].GetComponent<LockOnTarget>().ScreenPosition.x)
                    {
                        _aimAssistCAM.ApplyAimTarget(_assistTargets[i].transform);
                        _targetIndex = i;
                        _targetChanged = true;
                    }
                    _horizontalInput = 0;
                }
            }
        }
        else if (_horizontalInput != 0 && _targetChanged && _input.LookInput.x == 0)
        {
            _horizontalInput = 0;
            _targetChanged = false;
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
        DOnce(_isAimAssist, () =>
        {
            ApplyTargetToAssistCam();
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