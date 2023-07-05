using UnityEngine;

public class ChageColorItem : ItemBase3D
{
    [SerializeField, Header ("�����������̃v���C���ɃA�^�b�`����}�e���A��"), Tooltip ("�����������̃v���C���ɃA�^�b�`����}�e���A��")]
    Material _material;

    private void Start()
    {
        GetComponent<MeshRenderer> ().material = _material;
    }

    public override void GotItem()
    {
        MeshRenderer meshRend;
        meshRend = GameObject.FindGameObjectWithTag("Player").GetComponent<MeshRenderer>();
        meshRend.material = _material;
        this.gameObject.transform.position = Camera.main.transform.position;
        Destroy(this.gameObject, 1.5f);
    }
}
