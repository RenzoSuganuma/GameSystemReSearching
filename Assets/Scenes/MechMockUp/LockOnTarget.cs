using UnityEngine;
public class LockOnTarget : MonoBehaviour
{
    ACCAMComponent _acCam;
    private void Start()
    {
        _acCam = GameObject.FindAnyObjectByType<ACCAMComponent>();
    }
    private void OnBecameVisible()
    {
        _acCam.AddVisibleObjectInList(this.gameObject);
    }
    private void OnBecameInvisible()
    {
        _acCam.RemoveVisibleObjectInList(this.gameObject);
    }
}
