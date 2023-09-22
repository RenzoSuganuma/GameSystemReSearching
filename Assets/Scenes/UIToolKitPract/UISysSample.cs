using UnityEngine;
using CustomGamesUISystem;//自作の名前空間
using UnityEngine.UIElements;
using System;
using System.Collections;

public class UISysSample : MonoBehaviour
{
    [SerializeField] UIDocument _uIDocument;
    [SerializeField] string _uinameText, _uinameButton, _uinameProg, _uinameDropDown, _uinameEnum, _uinameRadButton;
    UILabel _uiLabel;
    UIButton _uiButton;
    UIProgBar _uiProgBar;

    UIDropDown _uIDropDown;
    Action func = () => { Debug.Log("VALUE CHANGED!!!!!!"); };
    Action func1 = () => { Debug.Log("ARE YOU OK!_?"); };
    UIEnumFeild _uiEnumFeild;

    MakeConversationWindow _win;
    string _content = "aaaaaaa\naaaaaaaaaaa\naaaaaaaaaaaa\nsdadasdasdas\nasdasda\nasdasdasdasda";
    bool _isspread = false, _isclose = false;
    private void Start()
    {
        this._uIDocument = GetComponent<UIDocument>();
        _win = new MakeConversationWindow(_uIDocument.rootVisualElement, "BOSS", _content, 25, 25, 50, 50);
        _win.SetWindowScale(new Vector2(0f, 0f));
        this._uiLabel = new UILabel(this._uIDocument.rootVisualElement, this._uinameText);
        this._uiButton = new UIButton(this._uIDocument.rootVisualElement, this._uinameButton);
        this._uiButton.AddButtonHandler(() => { Debug.Log("ButtonHandleTest"); });
        this._uiProgBar = new UIProgBar(this._uIDocument.rootVisualElement, this._uinameProg);
        this._uiProgBar.SetRangeOfValue(0, 100);
        this._uiProgBar.SetProgValue(50);
        this._uiEnumFeild = new UIEnumFeild(this._uIDocument.rootVisualElement, this._uinameEnum);
    }
    private void FixedUpdate()
    {
        if (this._isspread)
        {
            //if (_win != null)
                //_win.OpenWindow();
        }
        if(this._isclose)
        {
            //if(_win != null)
                //_win.CloseWindow();
        }
    }
    private void OnGUI()
    { 
        if (GUI.Button(new Rect(1, 1, 100, 100), "BUTTON"))
        {
            Debug.Log("会話ウィンドウ開く");
            _isspread = true;
            _isclose = false;
            _win.OpenWindow();
        }
        if (GUI.Button(new Rect(100, 500, 100, 100), "DIP"))
        {
            _isspread = false;
            _isclose = true;
            _win.CloseWindow();
        }
    }
}
