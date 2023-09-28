using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class OcculutionTarget : MonoBehaviour
{
    Material _material;
    Renderer _renderer;
    bool _isTransparent = false;
    public bool IsTransparent => _isTransparent;
    public Material Material => _material;
    private void Start()
    {
        _renderer = GetComponent<Renderer>();
        _material = _renderer.material;
    }
    private void Update()
    {
        _renderer.material = _material;
    }
    public void OverwriteMaterial(Material material)
    {
        _renderer.material = material;
    }
}
