using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BulletController : MonoBehaviour
{
    Rigidbody _rb;
    private void OnEnable()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.mass = 0;
        _rb.freezeRotation = true;
        Destroy(this.gameObject, 1);
    }
    private void FixedUpdate()
    {
        _rb.AddForce(this.gameObject.transform.forward);
    }
}
