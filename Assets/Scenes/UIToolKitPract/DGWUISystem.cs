using System;
using UnityEngine;
using UnityEngine.UIElements;
namespace DiscoveryGamesUISystem
{
    public class DGWUISystem { }
    /// <summary>
    /// UIToolKit��Label�������Ƃ��ɃC���X�^���X�����Ďg��
    /// </summary>
    public class UILabel
    {
        /// <summary> UIToolKit.Label �������ď��������� </summary>
        Label _label;
        public UILabel(VisualElement root, string uiName)
        {
            this._label = root.Q<Label>(uiName);
            this._label.text = "LABEL...";
        }
        /// <summary> �����̐ݒ� </summary>
        public void SetVisible(bool visible)
        {
            this._label.visible = visible;
        }
        /// <summary> �����̎擾 </summary>
        public bool GetVisible()
        {
            return this._label.visible;
        }/// <summary> �e�L�X�g�̐ݒ� </summary>
        public void SetText(string text)
        {
            this._label.text = text;
        }
    }
    /// <summary>
    /// UIToolKit��Button�������Ƃ��ɃC���X�^���X�����Ďg��
    /// </summary>
    public class UIButton
    {
        /// <summary> UIToolKit.Button �������ď��������� </summary>
        Button _button;
        public UIButton(VisualElement root, string uiName)
        {
            _button = root.Q<Button>(uiName);
            this._button.text = "BUTTON...";
        }
        /// <summary> �����̐ݒ� </summary>
        public void SetVisible(bool visible)
        {
            this._button.visible = visible;
        }
        /// <summary> �����̎擾 </summary>
        public bool GetVisible()
        {
            return this._button.visible;
        }
        /// <summary> �{�^���N���b�N���ɌĂяo�����֐��̓o�^ </summary>
        public void AddButtonHandler(Action buttonHandler)
        {
            this._button.clicked += buttonHandler;
        }
        /// <summary> �{�^���N���b�N���ɌĂяo�����֐��̓o�^���� </summary>
        public void RemoveButtonHandler(Action buttonHandler)
        {
            this._button.clicked -= buttonHandler;
        }
    }
    /// <summary>
    /// UIToolKit��ProgressBar�������Ƃ��ɃC���X�^���X�����Ďg��
    /// </summary>
    public class UIProgBar
    {
        /// <summary> UIToolKit.ProgressBar �������ď��������� </summary>
        ProgressBar _bar;
        public UIProgBar(VisualElement root, string uiName)
        {
            this._bar = root.Q<ProgressBar>(uiName);
            this._bar.title = "PROGBAR...";
        }
        /// <summary> �����̐ݒ� </summary>
        public void SetVisible(bool visible)
        {
            this._bar.visible = visible;
        }
        /// <summary> �����̎擾 </summary>
        public bool GetVisible()
        {
            return this._bar.visible;
        }
        /// <summary> �l�̂Ƃ�͈͂̐ݒ� </summary>
        public void SetRangeOfValue(int start, int end)
        {
            this._bar.lowValue = start;
            this._bar.highValue = end;
        }
        /// <summary> �l�̐ݒ� </summary>
        public void SetProgValue(int value)
        {
            this._bar.value = value;
        }
    }
}