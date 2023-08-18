using System;
using System.Collections.Generic;
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
    /// UIToolKitのScrollerを扱うときにインスタンス化して使う
    /// </summary>
    public class UIScroller : IInterface
    {
        /// <summary> UIToolKit.Scroller をつかって初期化する </summary>
        Scroller _scroller;
        public UIScroller(VisualElement root, string uiName)
        {
            _scroller = root.Q<Scroller>(uiName);
        }
        /// <summary> 可視性の設定 </summary>
        public void SetVisible(bool visible)
        {
            _scroller.visible = visible;
        }
        /// <summary> 可視性の取得 </summary>
        public bool GetVisible()
        {
            return (_scroller.visible);
        }
        /// <summary> 値のとる範囲の設定 </summary>
        public void SetRangeOfValue(int start, int end)
        {
            _scroller.lowValue = start;
            _scroller.highValue = end;
        }
        /// <summary> 値の設定 </summary>
        public void SetScrollValue(int value)
        {
            _scroller.value = value;
        }
    }
    /// <summary>
    /// UIToolKitのTextFeildを扱うときにインスタンス化して使う
    /// </summary>
    public class UITextFeild : IInterface
    {
        /// <summary> UIToolKit.TextFeild をつかって初期化する </summary>
        TextField _textField;
        public UITextFeild(VisualElement root, string uiName)
        {
            _textField = root.Q<TextField>(uiName);
            _textField.label = "TFEILD...";
        }
        /// <summary> 可視性の設定 </summary>
        public void SetVisible(bool visible)
        {
            _textField.visible = visible;
        }
        /// <summary> 可視性の取得 </summary>
        public bool GetVisible()
        {
            return (_textField.visible);
        }
        /// <summary> ラベルの設定 </summary>
        public void SetLabel(string label)
        {
            _textField.label = label;
        }
        /// <summary> 入力値の取得 </summary>
        public string GetValue()
        {
            return (_textField.value);
        }
    }
    /// <summary>
    /// UIToolKitのFoldoutを扱うときにインスタンス化して使う
    /// </summary>
    public class UIFoldOut : IInterface
    {
        /// <summary> UIToolKit.TextFeild をつかって初期化する </summary>
        Foldout _foldout;
        public UIFoldOut(VisualElement root,string uiName)
        {
            _foldout = root.Q<Foldout>(uiName);
            _foldout.text = "TFOLD...";
        }
        /// <summary> 可視性の設定 </summary>
        public void SetVisible(bool visible)
        {
            _foldout.visible = visible;
        }
        /// <summary> 可視性の取得 </summary>
        public bool GetVisible()
        {
            return (_foldout.visible);
        }
        /// <summary> 値の設定 </summary>
        public void SetValue(bool value)
        {
            _foldout.value = value;
        }
    }
    /// <summary>
    /// UIToolKitのSliderを扱うときにインスタンス化して使う
    /// </summary>
    public class UISlider : IInterface
    {
        /// <summary> UIToolKit.TextFeild をつかって初期化する </summary>
        Slider _slider;
        public UISlider(VisualElement root,string uiName)
        {
            _slider = root.Q<Slider>(uiName);
            _slider.label = "SLIDER...";
        }
        /// <summary> 可視性の設定 </summary>
        public void SetVisible(bool visible)
        {
            _slider.visible = visible;
        }
        /// <summary> 可視性の取得 </summary>
        public bool GetVisible()
        {
            return (_slider.visible);
        }
        /// <summary> 値のとる範囲の設定 </summary>
        public void SetRangeOfValue(float start, float end)
        {
            _slider.lowValue = start;
            _slider.highValue = end;
        }
        /// <summary> 値の設定 </summary>
        public void SetSliderValue(float value)
        {
            _slider.value = value;
        }
        /// <summary> 値の取得 </summary>
        public float GetSliderValue()
        {
            return (_slider.value);
        }
    }
    /// <summary>
    /// UIToolKitのSlider(Int)を扱うときにインスタンス化して使う
    /// </summary>
    public class UISliderInt : IInterface
    {
        /// <summary> UIToolKit.TextFeild をつかって初期化する </summary>
        SliderInt _slider;
        public UISliderInt(VisualElement root,string uiName)
        {
            _slider = root.Q<SliderInt>(uiName);
            _slider.label = "SLIDERINT...";
        }
        /// <summary> 可視性の設定 </summary>
        public void SetVisible(bool visible)
        {
            _slider.visible = visible;
        }
        /// <summary> 可視性の取得 </summary>
        public bool GetVisible()
        {
            return (_slider.visible);
        }
        /// <summary> 値のとる範囲の設定 </summary>
        public void SetRangeOfValue(int start, int end)
        {
            _slider.lowValue = start;
            _slider.highValue = end;
        }
        /// <summary> 値の設定 </summary>
        public void SetSliderValue(int value)
        {
            _slider.value = value;
        }
        /// <summary> 値の取得 </summary>
        public int GetSliderValue()
        {
            return (_slider.value);
        }
    }
    /// <summary>
    /// UIToolKitのMinMaxSliderを扱うときにインスタンス化して使う
    /// </summary>
    public class UIMinMaxSlider : IInterface
    {
        /// <summary> UIToolKit.MinMaxSlider をつかって初期化する </summary>
        MinMaxSlider _slider;
        public UIMinMaxSlider(VisualElement root,string uiName)
        {
            _slider = root.Q<MinMaxSlider>(uiName);
            _slider.label = "MinMaxSLIDER...";
        }
        /// <summary> 可視性の設定 </summary>
        public void SetVisible(bool visible)
        {
            _slider.visible = visible;
        }
        /// <summary> 可視性の取得 </summary>
        public bool GetVisible()
        {
            return (_slider.visible);
        }
        /// <summary> 値のとる範囲の設定 </summary>
        public void SetRangeOfValueLimit(float start, float end)
        {
            _slider.lowLimit = start;
            _slider.highLimit = end;
        }
        /// <summary> 値の設定 </summary>
        public void SetRangeOfValue(float start, float end)
        {
            _slider.minValue = start;
            _slider.maxValue = end;
        }
        /// <summary> 値の取得 </summary>
        public void GetSliderValue(ref float minV,ref float maxV)
        {
            minV = _slider.minValue;
            maxV = _slider.maxValue;
        }
    }
    /// <summary>
    /// UIToolKitのDropdownを扱うときにインスタンス化して使う
    /// </summary>
    public class UIDropDown : IInterface
    {
        DropdownField _dropFeild;
        Action valueChangedEvent;
        public UIDropDown(VisualElement root,string uiName)
        {
            _dropFeild = root.Q<DropdownField>(uiName);
            _dropFeild.RegisterValueChangedCallback((evt) => { ValueChangedEvent(); });
            _dropFeild.label = "DROPDOWN...";
        }
        /// <summary> ドロップダウンの値は選択された時に呼び出される </summary>
        void ValueChangedEvent() { valueChangedEvent?.Invoke(); }
        /// <summary> ドロップダウンの値は選択された時に呼び出される関数の登録 </summary>
        public void AddEventHandler(Action handler)
        {
            valueChangedEvent += handler;
        }
        /// <summary> ドロップダウンの値は選択された時に呼び出される関数の登録解除 </summary>
        public void RemoveEventHandler(Action handler)
        {
            valueChangedEvent -= handler;
        }
        /// <summary> リスト値の設定 </summary>
        public void SetChoices(List<string> list)
        {
            _dropFeild.choices = list;
        }
        /// <summary> リスト値の取得 </summary>
        public List<string> GetChoices()
        {
            return (_dropFeild.choices);
        }
        /// <summary> 値の取得 </summary>
        public string GetValue()
        {
            return (_dropFeild.value);
        }
        /// <summary> 可視性の設定 </summary>
        public void SetVisible(bool visible)
        {
            _dropFeild.visible = visible;
        }
        /// <summary> 可視性の取得 </summary>
        public bool GetVisible()
        {
            return (_dropFeild.visible);
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
            return (_bar.visible);
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