using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG;
using DG.Tweening;

public class DoTweenPrct : MonoBehaviour
{
    [SerializeField] Transform _targetTransform;
    private void Update()
    {
        //StartCoroutine(Start_());
        if (Input.GetKeyDown(KeyCode.Space))
        {
            transform.DOShakePosition(1f, 3f, 90, 30, false, true);
        }
    }
    IEnumerator Start_()
    {
        transform.DOLocalMove(_targetTransform.position, 1f);
        transform.DOLocalRotateQuaternion(Quaternion.AngleAxis(90f, Vector3.up), .25f)
            .SetLoops(4, LoopType.Incremental).SetEase(Ease.Linear);
        yield return new WaitForSeconds(.5f);
        transform.DOPause();
        yield return new WaitForSeconds(.5f);
        transform.DOPlay();
    }
}
