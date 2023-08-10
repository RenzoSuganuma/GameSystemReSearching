using UnityEngine;
using DiscoveryGamesUISystem;//é©çÏÇÃñºëOãÛä‘
using UnityEngine.UIElements;
public class UISysSample : MonoBehaviour
{
    [SerializeField] UIDocument _uIDocument;
    [SerializeField] string _uinameText, _uinameButton, _uinameProg;
    UILabel _uiLabel;
    UIButton _uiButton;
    UIProgBar _uiProgBar;
    private void Start()
    {
        this._uIDocument = GetComponent<UIDocument>();
        this._uiLabel = new UILabel(this._uIDocument.rootVisualElement,this._uinameText);
        this._uiLabel.SetText("TextSetTest");
        this._uiButton = new UIButton(this._uIDocument.rootVisualElement,this._uinameButton);
        this._uiButton.AddButtonHandler(() => { Debug.Log("ButtonHandleTest"); });
        this._uiProgBar = new UIProgBar(this._uIDocument.rootVisualElement, this._uinameProg);
        this._uiProgBar.SetRangeOfValue(0, 100);
        this._uiProgBar.SetProgValue(50);
    }
}
