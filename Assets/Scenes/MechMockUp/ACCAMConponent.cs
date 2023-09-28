using System.Collections.Generic;
using UnityEngine;
using RuntimeLog;
/// <summary>AC�̃J��������R���|�[�l���g</summary>
[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(Rigidbody))]
public class ACCAMComponentAlpha : MonoBehaviour
{
    /// <summary>�����^�C�����O</summary>
    RuntimeLogComponent _logComponent;
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
    /// <summary>�I�N���[�W����������̂ɃA�T�C�����铧���̕`�ʂ����邽�߂̃}�e���A��</summary>
    [SerializeField] Material _assignTransparentMaterial;
    /// <summary>�J�����ړ��ɕK�v�ȎO�p�֐��̃V�[�^�ɑΉ�����lX��</summary>
    float _thetaX = 0;
    /// <summary>�J�����ړ��ɕK�v�ȎO�p�֐��̃V�[�^�ɑΉ�����lY��</summary>
    float _thetaY = 0;
    /// <summary>���̓n���h���[</summary>
    ACInputHandler _inputHandler;
    void Start()
    {
        //���O�R���|�[�l���g�̃C���X�^���X��
        _logComponent = new(new Rect(0, 0, 100, 100));
        //���̓C���X�^���X��
        _inputHandler = GameObject.FindFirstObjectByType<ACInputHandler>();
        //NULL��������x�����O��f���o��
        if (GetComponent<Camera>() == null) Debug.LogWarning("�v���C���[�J������������Ȃ�");
        if (_centerTransform == null) Debug.LogWarning("�^�[�Q�b�g�̍��W��null����");
        if (GetComponent<Rigidbody>() == null) Debug.Log("���̃R���|�[�l���g���Ȃ�");
        //�^�[�Q�b�g������
        TargetingSequence(_centerTransform);
        //Rigidbody�v���p�e�B������ ���ꂪ�Ȃ��ƏՓ˂̔��肪�ł��Ȃ�
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        rigidbody.isKinematic = true;
        rigidbody.freezeRotation = true;
    }
    void Update()
    {
        //��]���� ����]
        RotateSequenceX();
        //�^�[�Q�b�g������
        TargetingSequence(_centerTransform);
        //�I�N���[�W����
        OcculusionPrepareSequence();
    }
    #region private���\�b�h
    /// <summary>�I�N���[�W�����ɕK�v�ȃv���p�e�B�Ȃǂ̏���</summary>
    private void OcculusionPrepareSequence()
    {
        //���S�Ƃ̋������Z�o
        var dis = Vector3.Distance(_centerTransform.position, this.transform.position);
        BoxCollider collider = GetComponent<BoxCollider>();
        //�e�p�����[�^�[�̏�����
        collider.isTrigger = true;
        collider.providesContacts = true;
        collider.center = new Vector3(0, 0, dis / 2f);
        collider.size = new Vector3(collider.size.x, collider.size.y, dis);
    }
    /// <summary>�I�N���[�W��������</summary>
    private void OcculusionSequence(Renderer renderer, OcculutionMode mode)
    {
        OcculutionTarget occTarget;
        switch (mode)
        {
            //�������������w�肳��Ă�Ƃ�
            case OcculutionMode.Transparent:
                {
                    //�����Ȃ��̂̃}�e���A��
                    renderer.material = _assignTransparentMaterial;
                    break;
                }
            //�����������������w�肳��Ă�Ƃ�
            case OcculutionMode.Normal:
                {
                    //�I�N���[�W�����^�[�Q�b�g��OccukutionTarget�R���|�[�l���g���A�^�b�`����Ă���΂��ꂪ�ێ�����ʏ�̃}�e���A���ɂ���
                    renderer.material = 
                        (renderer.gameObject.TryGetComponent<OcculutionTarget>(out occTarget))
                        ? occTarget.TargetMaterial : null;
                    break;
                }
        }
    }
    /// <summary>Y����]����</summary>
    private void RotateSequenceX()
    {
        //���͏���
        float inputX = _inputHandler.LookInput.x * _sencitivity.x * .01f;
        _thetaX += inputX;
        float inputY = _inputHandler.LookInput.y * _sencitivity.y * .01f;
        _thetaY += inputY;
        //���[���hZ����sin() ���[���hX����cos()
        //�O�p�֐����g���ĉ~�̋O�Ղ����ǂ点��B�e���̐����ɃI�t�Z�b�g���|����
        this.transform.position =
            new Vector3(Mathf.Sin(_thetaX) + _centerTransform.position.x + _offset.x
            , _centerTransform.position.y + (_camHeight * .1f) + _offset.y
            , Mathf.Cos(_thetaX) + _centerTransform.position.z + _offset.z) * _rotateRadius;
    }
    /// <summary>�ߑ�����</summary>
    private void TargetingSequence(Transform targetTransform)
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
    #region �Փ˔���
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
        {
            OcculusionSequence(other.GetComponent<Renderer>(), OcculutionMode.Transparent);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player"))
        {
            OcculusionSequence(other.GetComponent<Renderer>(), OcculutionMode.Normal);
        }
    }
    #endregion
    private void OnDrawGizmos()
    {
        //��]���a�̋����b�V���`��
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(_centerTransform.position, _rotateRadius);
    }
}
/// <summary>�I�N���[�W�������[�h</summary>
public enum OcculutionMode
{
    Normal,//���Ƃɖ߂��Ƃ��ɂ�����w��
    Transparent//�����ɂ���Ƃ��ɂ�����w��
}