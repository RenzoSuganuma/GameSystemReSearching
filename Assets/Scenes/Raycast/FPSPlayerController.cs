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

    // Start is called before the first frame update
    void Awake()
    {
        //CharacterController���擾
        if (this.GetComponent<CharacterController>() != null)
        {
            this._CHARACTERCONTROLLER = this.GetComponent<CharacterController>();
        }

        //�J�[�\���̃��b�N
        Cursor.lockState = CursorLockMode.Locked;

        //�Q�[����ʂ�`�ʂ��Ă���J�������擾
        this._camera = Camera.main;

        //�V�l�}�V�[�����z�J�����̎擾
        this._VIRTUALCAMERA = GameObject.FindGameObjectWithTag("VirtualCamera").GetComponent<CinemachineVirtualCamera>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //�}�E�X���x�ݒ�
        this._VIRTUALCAMERA.GetCinemachineComponent<CinemachinePOV>().m_HorizontalAxis.m_MaxSpeed = this._lookSen;
        this._VIRTUALCAMERA.GetCinemachineComponent<CinemachinePOV>().m_VerticalAxis.m_MaxSpeed = this._lookSen;

        //�N���X�w�A�\����\��
        if (this._PLAYERINPUTMODULE._isAiming && this._crossHair != null)
        {
            this._crossHair.SetActive(true);
        }
        else
        {
            this._crossHair.SetActive(false);
        }

        //�ړ��A���_�ړ��̃x���V�e�B���
        this._moveVel = this._PLAYERINPUTMODULE._moveVelocity.normalized;
        this._lookVel = this._PLAYERINPUTMODULE._lookVelocity;

        //�L�����ړ�
        this._CHARACTERCONTROLLER.Move(this.transform.forward * this._moveVel.y * this._moveSpd * Time.deltaTime);
        this._CHARACTERCONTROLLER.Move(this.transform.right * this._moveVel.x * this._moveSpd * Time.deltaTime);

        //�J�����̂����̉�]�ʂ��L�����̂����̉�]�ʂɑ��
        float camRot_Y;
        camRot_Y = this._camera.transform.rotation.y;

        float objRot_X, objRot_Z, objRot_W;
        objRot_X = this.gameObject.transform.rotation.x;
        objRot_Z = this.gameObject.transform.rotation.z;
        objRot_W = this.gameObject.transform.rotation.w;

        //�J�����̐��ʂ��L�����������Ă�����]�ʂ𗼕������傫���A�i�J�����̂�����]�ʁj�ɂ���
        this.gameObject.transform.forward = this._camera.transform.forward;
        this.gameObject.transform.rotation = new Quaternion(objRot_X,camRot_Y,objRot_Z,objRot_W);
    }

    private void OnGUI()
    {
        GUI.TextArea(new Rect(10, 10, 200, 100), $"{this._moveVel},{_lookVel}");
    }
}
