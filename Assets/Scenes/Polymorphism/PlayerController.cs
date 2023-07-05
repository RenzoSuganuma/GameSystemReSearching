using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerController : MonoBehaviour
{
    Camera _cam;
    NavMeshAgent _agent;
    [SerializeField,Header("エージェントの移動速度")] float _moveSpd = 3.5f;
    // Start is called before the first frame update
    void Start()
    {
        this._cam = FindFirstObjectByType<Camera>();
        this._agent = GetComponent<NavMeshAgent>();
        this._agent.speed = this._moveSpd;
        Debug.Log($"CAM : {this._cam.gameObject.name}");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = this._cam.ScreenPointToRay( Input.mousePosition );
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                this._agent.SetDestination( hit.point );
            }
        }
    }
}
