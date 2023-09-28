using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class OcculutionTarget : MonoBehaviour
{
    Material _material;
    Renderer _renderer;
    public Material Material => _material;
    private void Start()
    {
        _renderer = GetComponent<Renderer>();
        _material = _renderer.material;
    }
    public void OverwriteMaterial(Material material)
    {
        _renderer.material = material;
    }
}
