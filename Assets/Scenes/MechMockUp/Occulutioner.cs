using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Occulutioner : MonoBehaviour
{
    [SerializeField] Transform _centerTransform;
    [SerializeField] Material _transparentMaterial;
    GameObject _occuludedObject;
    /// <summary>�I�N���[�W��������</summary>
    public void OcculusionSequence()
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
                target.OverwriteMaterial(_transparentMaterial);
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
        }
    }
}