using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.UI;

/* 情報 */
/*
 * 作成：R菅沼
 */

/* 仕様構想 */
/*
 * 最終的にまとめて一つの名前空間に集約する？かもしれないので堅牢で安全性の高いコードにする(1)
 * できるだけシンプルな処理で終わらせる(2)
 */

/* コード説明 */
/* 
 * 
 */

/* 処理フロー */
/*
 * 
 */

public class QTEModule : MonoBehaviour
{
    /* パラメータ */
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
        //UI.Imageのコンポーネントの取得
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
            this._timeCount += Time.deltaTime;//時間の加算

        //膨張するUIがNULLでなくてかつ、ベースのUIよりスケールが小さい場合
        Transform //Transformの代入をして見やすく
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
