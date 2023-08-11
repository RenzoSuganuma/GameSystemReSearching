using System;
using UnityEngine;
using UnityEngine.UIElements;
namespace DiscoveryGamesUISystem
{
    interface IInterface
    {
        public void SetVisible(bool visible);
        public bool GetVisible();
    }

    public class DGWUISystem { }
    /// <summary>
    /// UIToolKitのLabelを扱うときにインスタンス化して使う
    /// </summary>
    public class UILabel : IInterface
    {
        /// <summary> UIToolKit.Label をつかって初期化する </summary>
        Label _label;
        public UILabel(VisualElement root, string uiName)
        {
            _label = root.Q<Label>(uiName);
            _label.text = "LABEL...";
        }
        public void SetVisible(bool visible)
        {
            _label.visible = visible;
        }
        public bool GetVisible()
        {
            return (_label.visible);
        }
        public void SetText(string text)
        {
            _label.text = text;
        }
    }
    /// <summary>
    /// UIToolKitのButtonを扱うときにインスタンス化して使う
    /// </summary>
    public class UIButton : IInterface
    {
        /// <summary> UIToolKit.Button をつかって初期化する </summary>
        Button _button;
        public UIButton(VisualElement root, string uiName)
        {
            _button = root.Q<Button>(uiName);
            _button.text = "BUTTON...";
        }
        public void SetVisible(bool visible)
        {
            _button.visible = visible;
        }
        public bool GetVisible()
        {
            return (_button.visible);
        }
        /// <summary> ボタンクリック時に呼び出される関数の登録 </summary>
        public void AddButtonHandler(Action buttonHandler)
        {
            _button.clicked += buttonHandler;
        }
        /// <summary> ボタンクリック時に呼び出される関数の登録解除 </summary>
        public void RemoveButtonHandler(Action buttonHandler)
        {
            _button.clicked -= buttonHandler;
        }
    }
    /// <summary>
    /// UIToolKitのToggleを扱うときにインスタンス化して使う
    /// </summary>
    public class UIToggle : IInterface
    {
        /// <summary> UIToolKit.Toggle をつかって初期化する </summary>
        Toggle _toggle;
        public UIToggle(VisualElement root, string uiName)
        {
            _toggle = root.Q<Toggle>(uiName);
            _toggle.text = "TOGGLE...";
        }
        public bool GetVisible()
        {
            return (_toggle.visible);
        }
        public void SetVisible(bool visible)
        {
            _toggle.visible = visible;
        }
        /// <summary> Toggleの値をbool型で返す </summary>
        /// <returns></returns>
        public bool GetToggle()
        {
            return (_toggle.value);
        }
    }

    /// <summary>
    /// UIToolKitのProgressBarを扱うときにインスタンス化して使う
    /// </summary>
    public class UIProgBar : IInterface
    {
        /// <summary> UIToolKit.ProgressBar をつかって初期化する </summary>
        ProgressBar _bar;
        public UIProgBar(VisualElement root, string uiName)
        {
            _bar = root.Q<ProgressBar>(uiName);
            _bar.title = "PROGBAR...";
        }
        /// <summary> 可視性の設定 </summary>
        public void SetVisible(bool visible)
        {
            _bar.visible = visible;
        }
        /// <summary> 可視性の取得 </summary>
        public bool GetVisible()
        {
            return _bar.visible;
        }
        /// <summary> 値のとる範囲の設定 </summary>
        public void SetRangeOfValue(int start, int end)
        {
            _bar.lowValue = start;
            _bar.highValue = end;
        }
        /// <summary> 値の設定 </summary>
        public void SetProgValue(int value)
        {
            _bar.value = value;
        }
    }
}