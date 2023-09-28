using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class OcculutionTarget : MonoBehaviour
{
    Material _material;
    public Material TargetMaterial { get { return _material; } }
    private void Start()
    {
        _material = GetComponent<Renderer>().material;     
    }
}
