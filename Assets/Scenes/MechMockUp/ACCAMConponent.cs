using System.Collections.Generic;
using UnityEngine;
using RuntimeLog;
/// <summary>AC�̃J��������R���|�[�l���g</summary>
public class ACCAMComponentAlpha : MonoBehaviour
{
    /// <summary>�����^�C�����O</summary>
    RuntimeLogComponent _logComponent;
    /// <summary>���̓n���h���[</summary>
    ACInputHandler _inputHandler;
    /// <summary>�I�N���[�W���������I�u�W�F�N�g���i�[���Ă���</summary>
    GameObject _occuludedObject;
    /// <summary>�J�����̒��S���W</summary>
    [SerializeField] Transform _centerTransform;
    /// <summary>�J�����ʒu�̃I�t�Z�b�g</summary>
    [SerializeField] Vector3 _offset;
    /// <summary>���͊��x</summary>
    [SerializeField] Vector2 _sencitivity;
    /// <summary>�J�����̍���</summary>
    [SerializeField] float _camHeight;
    /// <summary>��]���a</summary>
    [SerializeField] float _rotateRadius;
    /// <summary>��]���a</summary>
    [SerializeField, Range(.1f, .5f)] float _xAxisRotateAngleClampAbsoluteValue;
    /// <summary>��]�̔��]��L���ɂ��邩�̃t���O</summary>
    [SerializeField] bool _inverseRotation;
    /// <summary>�I�N���[�W����������̂ɃA�T�C�����铧���̕`�ʂ����邽�߂̃}�e���A��</summary>
    [SerializeField] Material _assignTransparentMaterial;
    /// <summary>�J�����ړ��ɕK�v�ȎO�p�֐��̃V�[�^�ɑΉ�����lX��</summary>
    float _thetaX = 0;
    /// <summary>�J�����ړ��ɕK�v�ȎO�p�֐��̃V�[�^�ɑΉ�����lY��</summary>
    float _thetaY = 0;
    void Start()
    {
        //���O�R���|�[�l���g�̃C���X�^���X��
        _logComponent = new(new Rect(0, 0, 100, 100));
        //���̓C���X�^���X��
        _inputHandler = GameObject.FindFirstObjectByType<ACInputHandler>();
        //NULL��������x�����O��f���o��
        if (GetComponent<Camera>() == null) Debug.LogWarning("�v���C���[�J������������Ȃ�");
        if (_centerTransform == null) Debug.LogWarning("�^�[�Q�b�g�̍��W��null����");
        if (GetComponent<Rigidbody>() == null) Debug.LogWarning("���̃R���|�[�l���g���Ȃ�");
        //�^�[�Q�b�g������
        TargettingSequence(_centerTransform);
    }
    void Update()
    {
        //��]���� ����]
        RotateSequence();
        //�^�[�Q�b�g������
        TargettingSequence(_centerTransform);
        //�I�N���[�W����
        OcculusionSequence();
    }
    #region private���\�b�h
    /// <summary>�I�N���[�W��������</summary>
    private void OcculusionSequence()
    {
        //�^�[�Q�b�g�Ƃ̋����̎Z�o
        var dis = Vector3.Distance(_centerTransform.position, this.transform.position);
        //�^�[�Q�b�g�Ɍ����������̃x�N�g���̎Z�o
        var dir = _centerTransform.position - this.transform.position;
        //�����̐���
        Ray ray = new(this.transform.position, dir);
        RaycastHit hit;
        Debug.DrawRay(ray.origin, ray.direction, Color.magenta, dis);
        //�����������ɓ���������
        if (Physics.Raycast(ray, out hit))
        {
            //�I�N���[�W��������
            if (hit.transform.gameObject.TryGetComponent<OcculutionTarget>(out OcculutionTarget target))
            {
                target.OverwriteMaterial(_assignTransparentMaterial);
                _occuludedObject = target.gameObject;
            }
            //�I�N���[�W������������
            else if (_occuludedObject != null)
            {
                if (_occuludedObject.TryGetComponent<OcculutionTarget>(out OcculutionTarget component))
                {
                    component.OverwriteMaterial(component.Material);
                }
            }
            //Debug.Log($"{nameof(OcculusionSequence)}:{hit.transform.gameObject.name}");
        }
    }
    /// <summary>Y����]����</summary>
    private void RotateSequence()
    {
        //���͏���
        float inputX = _inputHandler.LookInput.x * _sencitivity.x * .01f;
        _thetaX += inputX;
        float inputY = _inputHandler.LookInput.y * _sencitivity.y * .01f;
        _thetaY += inputY;
        //X����]�Ɏg�������̒l�̃N�����v
        _thetaY = Mathf.Clamp(_thetaY, -_xAxisRotateAngleClampAbsoluteValue, _xAxisRotateAngleClampAbsoluteValue);
        //��]�̔��]�̕����̏�����
        var sign = (_inverseRotation) ? -1 : 1;
        //���W�X�V
        this.transform.position =
            new Vector3(Mathf.Cos(_thetaX) * sign, Mathf.Sin(_thetaY) * sign, Mathf.Sin(_thetaX) * sign)
            * _rotateRadius + _centerTransform.position + _offset;
    }
    /// <summary>�ߑ�����</summary>
    private void TargettingSequence(Transform targetTransform)
    {
        //LookRotation�̑������ɐ��ʕ����̃x�N�g�����w�肵�ă^�[�Q�b�g�̃I�u�W�F�N�g������
        this.transform.rotation =
            Quaternion.LookRotation(targetTransform.position - this.transform.position
            , Vector3.up);
    }
    #endregion
    private void OnGUI()
    {
        //���O�̕`�ʕ\��
        _logComponent.DisplayLog("���O�o�̓e�X�g");
    }
    private void OnDrawGizmos()
    {
        //��]���a�̋����b�V���`��
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(_centerTransform.position, _rotateRadius);
    }
}