using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunFireModule : MonoBehaviour
{
    [SerializeField] private float rayDistance = 100f; // 光線の飛距離
    [SerializeField] private GameObject muzzle;
    [SerializeField] private GameObject muzzleTarget = null;

    LineRenderer lineRenderer;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    void Update()
    {
        // 画面中央の位置を取得
        Vector3 screenCenter = new Vector3(Screen.width / 2f, Screen.height / 2f, 0f);

        // メインカメラから画面中央への光線を作成
        Ray ray = Camera.main.ScreenPointToRay(screenCenter);

        // 光線との衝突を検出
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, rayDistance) && Input.GetMouseButtonDown(1))
        {
            // 光線が物体に衝突した場合の処理
            Debug.Log("Hit object: " + hit.collider.gameObject.name);
            Debug.Log("Hit object: " + hit.collider.gameObject.transform.position);
            muzzleTarget = hit.collider.gameObject;
        }

        Ray rayMuzlle = (muzzleTarget != null) ? new Ray(muzzle.transform.position, muzzleTarget.transform.position - muzzle.transform.position) : ray;

        RaycastHit hitMuzzle;

        if (Physics.Raycast(rayMuzlle, out hitMuzzle, rayDistance)&& Input.GetMouseButtonDown(1))
        {
            // 光線が物体に衝突した場合の処理
            Debug.Log("Hit object Muzzle: " + hitMuzzle.collider.gameObject.name);
            Debug.Log("Hit object Muzzle: " + hitMuzzle.collider.gameObject.transform.position);
        }

        // 光線を可視化するためにデバッグ表示
        Debug.DrawRay(ray.origin, ray.direction * Vector3.Distance(ray.origin, hit.collider.transform.position), Color.blue);
        Debug.DrawRay(rayMuzlle.origin, rayMuzlle.direction * Vector3.Distance(ray.origin, hit.collider.transform.position), Color.red);

        if (Input.GetMouseButton(1))
        {
            //LineRendererの各種パラメータの値設定
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