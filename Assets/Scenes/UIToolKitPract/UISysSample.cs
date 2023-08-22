using UnityEngine;
using DiscoveryGamesUISystem;//Ž©ì‚Ì–¼‘O‹óŠÔ
using UnityEngine.UIElements;
using System;
using System.Xml.Linq;

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
    int x, y;
    private void Start()
    {
        x = y = 1;
        this._uIDocument = GetComponent<UIDocument>();
        _win = new MakeConversationWindow(_uIDocument.rootVisualElement, "BOSS", _content, 25, 25, x, y);        
        //this._uiLabel = new UILabel(this._uIDocument.rootVisualElement,this._uinameText);
        //this._uiButton = new UIButton(this._uIDocument.rootVisualElement,this._uinameButton);
        //this._uiButton.AddButtonHandler(() => { Debug.Log("ButtonHandleTest"); });
        //this._uiProgBar = new UIProgBar(this._uIDocument.rootVisualElement, this._uinameProg);
        //this._uiProgBar.SetRangeOfValue(0, 100);
        //this._uiProgBar.SetProgValue(50);
        //this._uiEnumFeild = new UIEnumFeild(this._uIDocument.rootVisualElement, this._uinameEnum);
    }
    private void Update()
    {
        for (int i = 1; i < 20; ++i)
        {
            x++;
        }
        for (int j = 1; j < 20; ++j)
        {
            y++;
        }
    }
}
