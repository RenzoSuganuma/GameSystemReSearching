using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Unity.VisualScripting;

[RequireComponent(typeof(CharacterController))]
public class FPSPlayerController : MonoBehaviour
{
    /* properties */
    //MOVE PROPERTIES
    [SerializeField] float _moveSpd, _lookSen;
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

    //FOR CAM DISPLAY
    [SerializeField] private float _fov = 60;
    [SerializeField] private float _zoomRaito = 1;
    //public float 

    // Start is called before the first frame update
    void Awake()
    {
        //CharacterControllerを取得
        if (this.GetComponent<CharacterController>() != null)
        {
            this._CHARACTERCONTROLLER = this.GetComponent<CharacterController>();
        }

        //カーソルのロック
        Cursor.lockState = CursorLockMode.Locked;

        //ゲーム画面を描写しているカメラを取得
        this._camera = Camera.main;

        //シネマシーン仮想カメラの取得
        this._VIRTUALCAMERA = GameObject.FindGameObjectWithTag("VirtualCamera").GetComponent<CinemachineVirtualCamera>();

        this._lookSen *= Time.deltaTime;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //マウス感度設定
        this._VIRTUALCAMERA.GetCinemachineComponent<CinemachinePOV>().m_HorizontalAxis.m_MaxSpeed = this._lookSen;//インすペクタでのプロパティSpeed
        this._VIRTUALCAMERA.GetCinemachineComponent<CinemachinePOV>().m_VerticalAxis.m_MaxSpeed = this._lookSen;

        //クロスヘア表示非表示
        if (this._PLAYERINPUTMODULE._isAiming && this._crossHair != null)
        {
            this._crossHair.SetActive(true);
            this._VIRTUALCAMERA.m_Lens.FieldOfView = this._fov / this._zoomRaito;//ズーム
        }
        else
        {
            this._crossHair.SetActive(false);
            this._VIRTUALCAMERA.m_Lens.FieldOfView = this._fov;//ズームを等倍
        }

        //移動、視点移動のベロシティ代入
        this._moveVel = this._PLAYERINPUTMODULE._moveVelocity.normalized;
        this._lookVel = this._PLAYERINPUTMODULE._lookVelocity;

        //キャラ移動
        this._CHARACTERCONTROLLER.Move(this.transform.forward * this._moveVel.y * this._moveSpd * Time.deltaTime);
        this._CHARACTERCONTROLLER.Move(this.transform.right * this._moveVel.x * this._moveSpd * Time.deltaTime);

        //カメラのｙ軸の回転量をキャラのｙ軸の回転量に代入
        float camRot_Y;
        camRot_Y = this._camera.transform.rotation.y;

        float objRot_X, objRot_Z, objRot_W;
        objRot_X = this.gameObject.transform.rotation.x;
        objRot_Z = this.gameObject.transform.rotation.z;
        objRot_W = this.gameObject.transform.rotation.w;

        //カメラの正面をキャラも向いてｙ軸回転量を両方同じ大きさ、（カメラのｙ軸回転量）にする
        this.gameObject.transform.forward = this._camera.transform.forward;
        this.gameObject.transform.rotation = new Quaternion(objRot_X,camRot_Y,objRot_Z,objRot_W);
    }

    private void OnGUI()
    {
        GUI.TextArea(new Rect(10, 10, 200, 100), $"{this._moveVel},{_lookVel}");
    }
}
