using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Occulutioner : MonoBehaviour
{
    [SerializeField] Transform _centerTransform;
    [SerializeField] Material _transparentMaterial;
    GameObject _occuludedObject;
    /// <summary>オクルージョン処理</summary>
    public void OcculusionSequence()
    {
        //ターゲットとの距離の算出
        var dis = Vector3.Distance(_centerTransform.position, this.transform.position);
        //ターゲットに向かう向きのベクトルの算出
        var dir = _centerTransform.position - this.transform.position;
        //光線の生成
        Ray ray = new(this.transform.position, dir);
        RaycastHit hit;
        Debug.DrawRay(ray.origin, ray.direction, Color.magenta, dis);
        //光線が何かに当たったら
        if (Physics.Raycast(ray, out hit))
        {
            //オクルージョン処理
            if (hit.transform.gameObject.TryGetComponent<OcculutionTarget>(out OcculutionTarget target))
            {
                target.OverwriteMaterial(_transparentMaterial);
                _occuludedObject = target.gameObject;
            }
            //オクルージョン解除処理
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