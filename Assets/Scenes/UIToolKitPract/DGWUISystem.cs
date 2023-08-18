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
    /// UIToolKit��Label�������Ƃ��ɃC���X�^���X�����Ďg��
    /// </summary>
    public class UILabel : IInterface
    {
        /// <summary> UIToolKit.Label �������ď��������� </summary>
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
    /// UIToolKit��Button�������Ƃ��ɃC���X�^���X�����Ďg��
    /// </summary>
    public class UIButton : IInterface
    {
        /// <summary> UIToolKit.Button �������ď��������� </summary>
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
        /// <summary> �{�^���N���b�N���ɌĂяo�����֐��̓o�^ </summary>
        public void AddButtonHandler(Action buttonHandler)
        {
            _button.clicked += buttonHandler;
        }
        /// <summary> �{�^���N���b�N���ɌĂяo�����֐��̓o�^���� </summary>
        public void RemoveButtonHandler(Action buttonHandler)
        {
            _button.clicked -= buttonHandler;
        }
    }
    /// <summary>
    /// UIToolKit��Toggle�������Ƃ��ɃC���X�^���X�����Ďg��
    /// </summary>
    public class UIToggle : IInterface
    {
        /// <summary> UIToolKit.Toggle �������ď��������� </summary>
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
        /// <summary> Toggle�̒l��bool�^�ŕԂ� </summary>
        /// <returns></returns>
        public bool GetToggle()
        {
            return (_toggle.value);
        }
    }
    /// <summary>
    /// UIToolKit��Scroller�������Ƃ��ɃC���X�^���X�����Ďg��
    /// </summary>
    public class UIScroller : IInterface
    {
        /// <summary> UIToolKit.Scroller �������ď��������� </summary>
        Scroller _scroller;
        public UIScroller(VisualElement root, string uiName)
        {
            _scroller = root.Q<Scroller>(uiName);
        }
        /// <summary> �����̐ݒ� </summary>
        public void SetVisible(bool visible)
        {
            _scroller.visible = visible;
        }
        /// <summary> �����̎擾 </summary>
        public bool GetVisible()
        {
            return (_scroller.visible);
        }
        /// <summary> �l�̂Ƃ�͈͂̐ݒ� </summary>
        public void SetRangeOfValue(int start, int end)
        {
            _scroller.lowValue = start;
            _scroller.highValue = end;
        }
        /// <summary> �l�̐ݒ� </summary>
        public void SetScrollValue(int value)
        {
            _scroller.value = value;
        }
    }
    /// <summary>
    /// UIToolKit��TextFeild�������Ƃ��ɃC���X�^���X�����Ďg��
    /// </summary>
    public class UITextFeild : IInterface
    {
        /// <summary> UIToolKit.TextFeild �������ď��������� </summary>
        TextField _textField;
        public UITextFeild(VisualElement root, string uiName)
        {
            _textField = root.Q<TextField>(uiName);
            _textField.label = "TFEILD...";
        }
        /// <summary> �����̐ݒ� </summary>
        public void SetVisible(bool visible)
        {
            _textField.visible = visible;
        }
        /// <summary> �����̎擾 </summary>
        public bool GetVisible()
        {
            return (_textField.visible);
        }
        /// <summary> ���x���̐ݒ� </summary>
        public void SetLabel(string label)
        {
            _textField.label = label;
        }
        /// <summary> ���͒l�̎擾 </summary>
        public string GetValue()
        {
            return (_textField.value);
        }
    }
    /// <summary>
    /// UIToolKit��Foldout�������Ƃ��ɃC���X�^���X�����Ďg��
    /// </summary>
    public class UIFoldOut : IInterface
    {
        /// <summary> UIToolKit.TextFeild �������ď��������� </summary>
        Foldout _foldout;
        public UIFoldOut(VisualElement root,string uiName)
        {
            _foldout = root.Q<Foldout>(uiName);
            _foldout.text = "TFOLD...";
        }
        /// <summary> �����̐ݒ� </summary>
        public void SetVisible(bool visible)
        {
            _foldout.visible = visible;
        }
        /// <summary> �����̎擾 </summary>
        public bool GetVisible()
        {
            return (_foldout.visible);
        }
        /// <summary> �l�̐ݒ� </summary>
        public void SetValue(bool value)
        {
            _foldout.value = value;
        }
    }
    /// <summary>
    /// UIToolKit��Slider�������Ƃ��ɃC���X�^���X�����Ďg��
    /// </summary>
    public class UISlider : IInterface
    {
        /// <summary> UIToolKit.TextFeild �������ď��������� </summary>
        Slider _slider;
        public UISlider(VisualElement root,string uiName)
        {
            _slider = root.Q<Slider>(uiName);
            _slider.label = "SLIDER...";
        }
        /// <summary> �����̐ݒ� </summary>
        public void SetVisible(bool visible)
        {
            _slider.visible = visible;
        }
        /// <summary> �����̎擾 </summary>
        public bool GetVisible()
        {
            return (_slider.visible);
        }
        /// <summary> �l�̂Ƃ�͈͂̐ݒ� </summary>
        public void SetRangeOfValue(float start, float end)
        {
            _slider.lowValue = start;
            _slider.highValue = end;
        }
        /// <summary> �l�̐ݒ� </summary>
        public void SetSliderValue(float value)
        {
            _slider.value = value;
        }
        /// <summary> �l�̎擾 </summary>
        public float GetSliderValue()
        {
            return (_slider.value);
        }
    }
    /// <summary>
    /// UIToolKit��Slider(Int)�������Ƃ��ɃC���X�^���X�����Ďg��
    /// </summary>
    public class UISliderInt : IInterface
    {
        /// <summary> UIToolKit.TextFeild �������ď��������� </summary>
        SliderInt _slider;
        public UISliderInt(VisualElement root,string uiName)
        {
            _slider = root.Q<SliderInt>(uiName);
            _slider.label = "SLIDERINT...";
        }
        /// <summary> �����̐ݒ� </summary>
        public void SetVisible(bool visible)
        {
            _slider.visible = visible;
        }
        /// <summary> �����̎擾 </summary>
        public bool GetVisible()
        {
            return (_slider.visible);
        }
        /// <summary> �l�̂Ƃ�͈͂̐ݒ� </summary>
        public void SetRangeOfValue(int start, int end)
        {
            _slider.lowValue = start;
            _slider.highValue = end;
        }
        /// <summary> �l�̐ݒ� </summary>
        public void SetSliderValue(int value)
        {
            _slider.value = value;
        }
        /// <summary> �l�̎擾 </summary>
        public int GetSliderValue()
        {
            return (_slider.value);
        }
    }
    /// <summary>
    /// UIToolKit��MinMaxSlider�������Ƃ��ɃC���X�^���X�����Ďg��
    /// </summary>
    public class UIMinMaxSlider : IInterface
    {
        /// <summary> UIToolKit.MinMaxSlider �������ď��������� </summary>
        MinMaxSlider _slider;
        public UIMinMaxSlider(VisualElement root,string uiName)
        {
            _slider = root.Q<MinMaxSlider>(uiName);
            _slider.label = "MinMaxSLIDER...";
        }
        /// <summary> �����̐ݒ� </summary>
        public void SetVisible(bool visible)
        {
            _slider.visible = visible;
        }
        /// <summary> �����̎擾 </summary>
        public bool GetVisible()
        {
            return (_slider.visible);
        }
        /// <summary> �l�̂Ƃ�͈͂̐ݒ� </summary>
        public void SetRangeOfValueLimit(float start, float end)
        {
            _slider.lowLimit = start;
            _slider.highLimit = end;
        }
        /// <summary> �l�̐ݒ� </summary>
        public void SetRangeOfValue(float start, float end)
        {
            _slider.minValue = start;
            _slider.maxValue = end;
        }
        /// <summary> �l�̎擾 </summary>
        public void GetSliderValue(ref float minV,ref float maxV)
        {
            minV = _slider.minValue;
            maxV = _slider.maxValue;
        }
    }
    /// <summary>
    /// UIToolKit��Dropdown�������Ƃ��ɃC���X�^���X�����Ďg��
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
        /// <summary> �h���b�v�_�E���̒l�͑I�����ꂽ���ɌĂяo����� </summary>
        void ValueChangedEvent() { valueChangedEvent?.Invoke(); }
        /// <summary> �h���b�v�_�E���̒l�͑I�����ꂽ���ɌĂяo�����֐��̓o�^ </summary>
        public void AddEventHandler(Action handler)
        {
            valueChangedEvent += handler;
        }
        /// <summary> �h���b�v�_�E���̒l�͑I�����ꂽ���ɌĂяo�����֐��̓o�^���� </summary>
        public void RemoveEventHandler(Action handler)
        {
            valueChangedEvent -= handler;
        }
        /// <summary> ���X�g�l�̐ݒ� </summary>
        public void SetChoices(List<string> list)
        {
            _dropFeild.choices = list;
        }
        /// <summary> ���X�g�l�̎擾 </summary>
        public List<string> GetChoices()
        {
            return (_dropFeild.choices);
        }
        /// <summary> �l�̎擾 </summary>
        public string GetValue()
        {
            return (_dropFeild.value);
        }
        /// <summary> �����̐ݒ� </summary>
        public void SetVisible(bool visible)
        {
            _dropFeild.visible = visible;
        }
        /// <summary> �����̎擾 </summary>
        public bool GetVisible()
        {
            return (_dropFeild.visible);
        }
    }
    /// <summary>
    /// UIToolKit��ProgressBar�������Ƃ��ɃC���X�^���X�����Ďg��
    /// </summary>
    public class UIProgBar : IInterface
    {
        /// <summary> UIToolKit.ProgressBar �������ď��������� </summary>
        ProgressBar _bar;
        public UIProgBar(VisualElement root, string uiName)
        {
            _bar = root.Q<ProgressBar>(uiName);
            _bar.title = "PROGBAR...";
        }
        /// <summary> �����̐ݒ� </summary>
        public void SetVisible(bool visible)
        {
            _bar.visible = visible;
        }
        /// <summary> �����̎擾 </summary>
        public bool GetVisible()
        {
            return (_bar.visible);
        }
        /// <summary> �l�̂Ƃ�͈͂̐ݒ� </summary>
        public void SetRangeOfValue(int start, int end)
        {
            _bar.lowValue = start;
            _bar.highValue = end;
        }
        /// <summary> �l�̐ݒ� </summary>
        public void SetProgValue(int value)
        {
            _bar.value = value;
        }
    }
}