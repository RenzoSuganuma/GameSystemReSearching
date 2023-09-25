using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class ACCAMComponentAlpha : MonoBehaviour
{
    // Quarertion q = vector * sin theta/2
    //Degree deg = (q/vector)
    [SerializeField] Transform _target;
    void Start()
    {
        //this.transform.LookAt(_target);//X=26.528,Y=49.651Å®ìxêîñ@
        //(0.20824, 0.40865, -0.09633, 0.88338)Å®élå≥êî
    }

    void FixedUpdate()
    {
        var q = transform.rotation;
        //var v = (_target.position - this.transform.position).normalized;
        //var theta = q.x / v.x;
        Debug.Log($"{q.ToString()}");
    }
}
