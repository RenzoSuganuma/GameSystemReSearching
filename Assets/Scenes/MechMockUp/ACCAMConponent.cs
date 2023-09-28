using System.Collections.Generic;
using UnityEngine;
using RuntimeLog;
using Unity.VisualScripting;

[RequireComponent(typeof(BoxCollider))]
public class ACCAMComponentAlpha : MonoBehaviour
{
    [SerializeField] Transform _target;
    [SerializeField] Vector3 _offset;
    [SerializeField] Vector2 _sencitivity;
    [SerializeField] float _camHeight;
    [SerializeField] float _rotateRadius;
    [SerializeField] float _camLockOnDistance;
    RuntimeLogComponent _logComponent;
    float _thetaX = 0;
    float _thetaY = 0;
    void Start()
    {
        //���O�R���|�[�l���g�̃C���X�^���X��
        _logComponent = new(new Rect(0, 0, 100, 100));
        //NULL��������x�����O��f���o��
        if (GetComponent<Camera>() == null) Debug.LogWarning("�v���C���[�J������������Ȃ�");
        //�^�[�Q�b�g������
        TargetingSequence();
    }
    void Update()
    {
        float inputX = Input.GetAxis("Mouse X") * _sencitivity.x * .01f;
        _thetaX += inputX;
        float inputY = Input.GetAxis("Mouse Y") * _sencitivity.y * .01f;
        _thetaY += inputY;
        //���[���hZ����sin() ���[���hX����cos()
        //��]���� ����]
        RotateSequenceX();
        //�^�[�Q�b�g������
        TargetingSequence();
        //�I�N���[�W����
        OcculusionSequence();
        //���b�N�I���͈͓��̃I�u�W�F�N�g������
    }
    private void OcculusionSequence()
    {
        var dis = Vector3.Distance(_target.position, this.transform.position);
        BoxCollider collider = GetComponent<BoxCollider>();
        collider.isTrigger = true;
        collider.providesContacts = true;
        collider.center = new Vector3(0, 0, dis / 2f);
        collider.size = new Vector3(collider.size.x, collider.size.y, dis);
    }
    private void RotateSequenceX()
    {
        this.transform.position =
            new Vector3(Mathf.Sin(_thetaX) + _target.position.x + _offset.x
            , _target.position.y + (_camHeight * .1f) + _offset.y
            , Mathf.Cos(_thetaX) + _target.position.z + _offset.z) * _rotateRadius;
    }
    private void TargetingSequence()
    {
        this.transform.rotation =
            Quaternion.LookRotation(_target.transform.position - this.transform.position
            , Vector3.up);
    }
    private void OnGUI()
    {
        //���O�̕`�ʕ\��
        _logComponent.DisplayLog("���O�o�̓e�X�g");
    }
    private void OnTriggerStay(Collider other)
    {
    }
    private void OnTriggerExit(Collider other)
    {
    }
    private void OnDrawGizmos()
    {
        //��]���a�̋����b�V���`��
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(_target.position, _rotateRadius);
    }
}
