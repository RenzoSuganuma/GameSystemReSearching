using UnityEngine;

public class StackItem : ItemBase3D
{
    public override void GotItem()
    {
        this.gameObject.transform.parent = GameObject.FindGameObjectWithTag("Player").transform;
    }
}
