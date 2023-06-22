using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.UI;

/* ��� */
/*
 * �쐬�FR����
 */

/* �d�l�\�z */
/*
 * �ŏI�I�ɂ܂Ƃ߂Ĉ�̖��O��ԂɏW�񂷂�H��������Ȃ��̂Ō��S�ň��S���̍����R�[�h�ɂ���(1)
 * �ł��邾���V���v���ȏ����ŏI��点��(2)
 */

/* �R�[�h���� */
/* 
 * 
 */

/* �����t���[ */
/*
 * 
 */

public class QTEModule : MonoBehaviour
{
    /* �p�����[�^ */
    UnityEngine.UI.Image _uiImageExpansion;
    UnityEngine.UI.Text  _uiTextExpansion;

                     private float _timeCount = 0f;//[sec]
    [SerializeField] private float _timeLimit = 0f;

    [SerializeField] private int   _pressCount = 0;

    [SerializeField] private bool  _isQTE = false;

    private GameObject _uiExpansion, _uiExpText;

    // Start is called before the first frame update
    void Start()
    {
        //UI.Image�̃R���|�[�l���g�̎擾
        this._uiExpansion = GameObject.Find("QTEExpansion");
            this._uiImageExpansion = this._uiExpansion.GetComponent<UnityEngine.UI.Image>();
        this._uiExpText = GameObject.Find("QTEText");
            this._uiTextExpansion = this._uiExpText.GetComponent<UnityEngine.UI.Text>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && _isQTE)
        {
            this._pressCount++;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_isQTE)
            this._timeCount += Time.deltaTime;//���Ԃ̉��Z

        //�c������UI��NULL�łȂ��Ă��A�x�[�X��UI���X�P�[�����������ꍇ
        Transform //Transform�̑�������Č��₷��
            uiExpansionTrans = this._uiExpansion.transform;

        if (this._pressCount >= 20)
        {
            this._uiTextExpansion.color = UnityEngine.Color.blue;
            this._uiTextExpansion.text = "YOU DID IT!";
            this._isQTE = false;
        }
        else
        {
            this._uiTextExpansion.text = "PRESS E KEY!";
        }

        if (this._timeLimit < this._timeCount && _isQTE)
        {
            this._uiImageExpansion.color = UnityEngine.Color.red;
        }
        else
        {
            this._uiImageExpansion.color = UnityEngine.Color.yellow;
            uiExpansionTrans.localScale = Vector3.one * this._timeCount;
        }
    }

    public void StartQTE()
    {
        this._isQTE = true;
    }
}
