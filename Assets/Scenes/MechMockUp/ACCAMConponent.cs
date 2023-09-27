using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class ACCAMComponentAlpha : MonoBehaviour
{
    [SerializeField] Transform _target;
    float _theta = 0;
    void Start()
    {
        //NULLだったら何もしない
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
        //ワールドZ軸→sin() ワールドX軸→cos()
        //回転処理 横回転
        this.transform.position = new Vector3(Mathf.Sin(_theta), 3, Mathf.Cos(_theta)) * 5;
        //ターゲットを向く
        this.transform.rotation =
            Quaternion.LookRotation(_target.transform.position - this.transform.position
            , Vector3.up);
    }
}
