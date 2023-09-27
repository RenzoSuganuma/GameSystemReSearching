using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class ACCAMComponentAlpha : MonoBehaviour
{
    [SerializeField] Transform _target;
    float _theta = 0;
    void Start()
    {
        //NULL�������牽�����Ȃ�
        if (GetComponent<Camera>() == null) return;
        this.transform.rotation = 
            Quaternion.LookRotation(_target.transform.position - this.transform.position
            , Vector3.up);
    }
    void Update()
    {
        float inputX = Input.GetAxis("Mouse X");
        _theta += inputX;
        float inputY = Input.GetAxis("Mouse Y");
        //���[���hZ����sin() ���[���hX����cos()
        //��]���� ����]
        this.transform.position = new Vector3(Mathf.Sin(_theta), 3, Mathf.Cos(_theta)) * 5;
        //�^�[�Q�b�g������
        this.transform.rotation =
            Quaternion.LookRotation(_target.transform.position - this.transform.position
            , Vector3.up);
    }
}
