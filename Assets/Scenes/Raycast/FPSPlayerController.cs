using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

[RequireComponent(typeof(CharacterController))]
public class FPSPlayerController : MonoBehaviour
{
    /* properties */
    //MOVE PROPERTIES
    [SerializeField] float _moveSpd;
    private Vector2 _moveVel, _lookVel;

    //SHOOTING PROPERTIES
    [SerializeField] GameObject _crossHair;

    /* nesessary component to process this code order */
    //COMPONENTS TO MOVE
    [SerializeField] private PlayerInputModule _PLAYERINPUTMODULE;
    private CharacterController _CHARACTERCONTROLLER;

    //FOR CAMERA MOVING
    [SerializeField] private CinemachineVirtualCamera _VIRTUALCAMERA;
    private Camera _camera;

    // Start is called before the first frame update
    void Awake()
    {
        if(this.GetComponent<CharacterController>() != null)
        {
            this._CHARACTERCONTROLLER = this.GetComponent<CharacterController>();
        }

        Cursor.lockState = CursorLockMode.Locked;

        this._camera = Camera.main;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (this._PLAYERINPUTMODULE._isAiming && this._crossHair != null)
        {
            this._crossHair.SetActive(true);
        }
        else
        {
            this._crossHair.SetActive(false);
        }

        this._moveVel = this._PLAYERINPUTMODULE._moveVelocity.normalized;
        this._lookVel = this._PLAYERINPUTMODULE._lookVelocity.normalized;

        this._CHARACTERCONTROLLER.Move(this.transform.forward * this._moveVel.y * this._moveSpd * Time.deltaTime);
        this._CHARACTERCONTROLLER.Move(this.transform.right * this._moveVel.x * this._moveSpd * Time.deltaTime);

        float camRot_Y;
        camRot_Y = this._camera.transform.rotation.y;

        float objRot_X, objRot_Z, objRot_W;
        objRot_X = this.gameObject.transform.rotation.x;
        objRot_Z = this.gameObject.transform.rotation.z;
        objRot_W = this.gameObject.transform.rotation.w;

        this.gameObject.transform.forward = this._camera.transform.forward;
        this.gameObject.transform.rotation = new Quaternion(objRot_X,camRot_Y,objRot_Z,objRot_W);
    }

    private void OnGUI()
    {
        GUI.TextArea(new Rect(10, 10, 200, 100), $"{this._moveVel},{_lookVel}");
    }
}
