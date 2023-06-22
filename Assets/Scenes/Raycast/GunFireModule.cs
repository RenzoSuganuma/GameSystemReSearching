using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunFireModule : MonoBehaviour
{
    [SerializeField] private float rayDistance = 100f; // �����̔򋗗�
    [SerializeField] private GameObject muzzle;
    [SerializeField] private GameObject muzzleTarget = null;

    LineRenderer lineRenderer;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    void Update()
    {
        // ��ʒ����̈ʒu���擾
        Vector3 screenCenter = new Vector3(Screen.width / 2f, Screen.height / 2f, 0f);

        // ���C���J���������ʒ����ւ̌������쐬
        Ray ray = Camera.main.ScreenPointToRay(screenCenter);

        // �����Ƃ̏Փ˂����o
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, rayDistance) && Input.GetMouseButtonDown(1))
        {
            // ���������̂ɏՓ˂����ꍇ�̏���
            Debug.Log("Hit object: " + hit.collider.gameObject.name);
            Debug.Log("Hit object: " + hit.collider.gameObject.transform.position);
            muzzleTarget = hit.collider.gameObject;
        }

        Ray rayMuzlle = (muzzleTarget != null) ? new Ray(muzzle.transform.position, muzzleTarget.transform.position - muzzle.transform.position) : ray;

        RaycastHit hitMuzzle;

        if (Physics.Raycast(rayMuzlle, out hitMuzzle, rayDistance)&& Input.GetMouseButtonDown(1))
        {
            // ���������̂ɏՓ˂����ꍇ�̏���
            Debug.Log("Hit object Muzzle: " + hitMuzzle.collider.gameObject.name);
            Debug.Log("Hit object Muzzle: " + hitMuzzle.collider.gameObject.transform.position);
        }

        // �������������邽�߂Ƀf�o�b�O�\��
        Debug.DrawRay(ray.origin, ray.direction * Vector3.Distance(ray.origin, hit.collider.transform.position), Color.blue);
        Debug.DrawRay(rayMuzlle.origin, rayMuzlle.direction * Vector3.Distance(ray.origin, hit.collider.transform.position), Color.red);

        if (Input.GetMouseButton(1))
        {
            //LineRenderer�̊e��p�����[�^�̒l�ݒ�
            lineRenderer.SetPosition(0, rayMuzlle.origin);
            lineRenderer.SetPosition(1, hitMuzzle.collider.transform.position);
            lineRenderer.startWidth = .1f;
            lineRenderer.endWidth = .1f;
        }
        else
        {
            lineRenderer.startWidth = lineRenderer.endWidth = 0f;
        }
    }
}