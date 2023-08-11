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
        public void SetText(string text)
        {
            _label.text = text;
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
            return _bar.visible;
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