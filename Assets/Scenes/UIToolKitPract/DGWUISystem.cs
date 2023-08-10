using System;
using UnityEngine;
using UnityEngine.UIElements;
namespace DiscoveryGamesUISystem
{
    public class DGWUISystem { }
    /// <summary>
    /// UIToolKitのLabelを扱うときにインスタンス化して使う
    /// </summary>
    public class UILabel
    {
        /// <summary> UIToolKit.Label をつかって初期化する </summary>
        Label _label;
        public UILabel(VisualElement root, string uiName)
        {
            this._label = root.Q<Label>(uiName);
            this._label.text = "LABEL...";
        }
        /// <summary> 可視性の設定 </summary>
        public void SetVisible(bool visible)
        {
            this._label.visible = visible;
        }
        /// <summary> 可視性の取得 </summary>
        public bool GetVisible()
        {
            return this._label.visible;
        }/// <summary> テキストの設定 </summary>
        public void SetText(string text)
        {
            this._label.text = text;
        }
    }
    /// <summary>
    /// UIToolKitのButtonを扱うときにインスタンス化して使う
    /// </summary>
    public class UIButton
    {
        /// <summary> UIToolKit.Button をつかって初期化する </summary>
        Button _button;
        public UIButton(VisualElement root, string uiName)
        {
            _button = root.Q<Button>(uiName);
            this._button.text = "BUTTON...";
        }
        /// <summary> 可視性の設定 </summary>
        public void SetVisible(bool visible)
        {
            this._button.visible = visible;
        }
        /// <summary> 可視性の取得 </summary>
        public bool GetVisible()
        {
            return this._button.visible;
        }
        /// <summary> ボタンクリック時に呼び出される関数の登録 </summary>
        public void AddButtonHandler(Action buttonHandler)
        {
            this._button.clicked += buttonHandler;
        }
        /// <summary> ボタンクリック時に呼び出される関数の登録解除 </summary>
        public void RemoveButtonHandler(Action buttonHandler)
        {
            this._button.clicked -= buttonHandler;
        }
    }
    /// <summary>
    /// UIToolKitのProgressBarを扱うときにインスタンス化して使う
    /// </summary>
    public class UIProgBar
    {
        /// <summary> UIToolKit.ProgressBar をつかって初期化する </summary>
        ProgressBar _bar;
        public UIProgBar(VisualElement root, string uiName)
        {
            this._bar = root.Q<ProgressBar>(uiName);
            this._bar.title = "PROGBAR...";
        }
        /// <summary> 可視性の設定 </summary>
        public void SetVisible(bool visible)
        {
            this._bar.visible = visible;
        }
        /// <summary> 可視性の取得 </summary>
        public bool GetVisible()
        {
            return this._bar.visible;
        }
        /// <summary> 値のとる範囲の設定 </summary>
        public void SetRangeOfValue(int start, int end)
        {
            this._bar.lowValue = start;
            this._bar.highValue = end;
        }
        /// <summary> 値の設定 </summary>
        public void SetProgValue(int value)
        {
            this._bar.value = value;
        }
    }
}