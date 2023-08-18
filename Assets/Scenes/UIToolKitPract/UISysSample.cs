using UnityEngine;
using DiscoveryGamesUISystem;//Ž©ì‚Ì–¼‘O‹óŠÔ
using UnityEngine.UIElements;
using System;

public class UISysSample : MonoBehaviour
{
    [SerializeField] UIDocument _uIDocument;
    [SerializeField] string _uinameText, _uinameButton, _uinameProg, _uinameDropDown;
    UILabel _uiLabel;
    UIButton _uiButton;
    UIProgBar _uiProgBar;

    UIDropDown _uIDropDown;
    Action func = () => { Debug.Log("VALUE CHANGED!!!!!!"); };
    Action func1 = () => { Debug.Log("ARE YOU OK!_?"); };
    private void Start()
    {
        this._uIDocument = GetComponent<UIDocument>();
        this._uiLabel = new UILabel(this._uIDocument.rootVisualElement,this._uinameText);
        this._uiButton = new UIButton(this._uIDocument.rootVisualElement,this._uinameButton);
        this._uiButton.AddButtonHandler(() => { Debug.Log("ButtonHandleTest"); });
        this._uiProgBar = new UIProgBar(this._uIDocument.rootVisualElement, this._uinameProg);
        this._uiProgBar.SetRangeOfValue(0, 100);
        this._uiProgBar.SetProgValue(50);
        this._uIDropDown = new UIDropDown(this._uIDocument.rootVisualElement, this._uinameDropDown);
        this._uIDropDown.AddEventHandler(func);
        this._uIDropDown.AddEventHandler(func1);
        this._uIDropDown.SetChoices(new System.Collections.Generic.List<string> { "a"});
    }
}
