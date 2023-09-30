using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
#region README
//Unity UIToolKit�����p�����Q�[���J����⏕������̂Ƃ��Ė{�X�N���v�g�������܂����B
//UIToolKit��Controls��RadioButton,Group�ȊO�̗v�f�Ȃ爵���܂�
//Ver 1.0.1
#endregion
namespace CustomGamesUISystemAlpha
{
    public class CustomUISystemAlpha { }
    #region UIToolKit_Controls
    interface IInterface
    {
        public void SetVisible(bool visible);
        public bool GetVisible();
    }
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
    /// UIToolKit��Enum�������Ƃ��ɃC���X�^���X�����Ďg��
    /// </summary>
    public class UIEnumFeild : IInterface
    {
        EnumField _enumFeild;
        Action valueChangedEvent;
        public UIEnumFeild(VisualElement root,string uiName)
        {
            _enumFeild = root.Q<EnumField>(uiName);
            _enumFeild.RegisterValueChangedCallback((evt) => { ValueChangedEvent(); });
            _enumFeild.label = "ENUM...";
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
        /// <summary> �l�̎擾 </summary>
        public Enum GetValue()
        {
            return (_enumFeild.value);
        }
        /// <summary> �����̐ݒ� </summary>
        public void SetVisible(bool visible)
        {
            _enumFeild.visible = visible;
        }
        /// <summary> �����̎擾 </summary>
        public bool GetVisible()
        {
            return (_enumFeild.visible);
        }
    }
    #endregion
    #region �J�X�^����UI
    /// <summary>
    /// ��b�E�B���h�E�������Ƃ��ɃC���X�^���X�����Ďg��
    /// </summary>
    public class MakeConversationWindow
    {
        //�O���[�v�{�b�N�X�̃C���X�^���X��
        GroupBox window = new GroupBox();
        //�v���p�e�B�������̉ǐ��̂���Istyle�̐錾�Ɗi�[
        IStyle windowStyle;
        //�b�҃��x���̃C���X�^���X��
        Label speakerNameLabel = new Label();
        //�v���p�e�B�������̉ǐ��̂���Istyle�̐錾�Ɗi�[
        IStyle speakerLabStyle;
        //��b���x���̃C���X�^���X��
        Label conversationTextLabel = new Label();
        //�v���p�e�B�������̉ǐ��̂���Istyle�̐錾�Ɗi�[
        IStyle convLabStyle;
        //root���R���X�g���N�^�O����ł������肽�����ߐ錾
        VisualElement root;
        //�E�B���h�E���J���ꂽ�Ƃ��ɌĂяo�����
        public Action WindowOpened = default;
        //�E�B���h�E������ꂽ�Ƃ��Ăяo�����
        public Action WindowClosed = default;
        //��b�E�B���h�E�̕��ƍ���
        float _convWindowWinWidth, _convWindowWinHeight;
        /// <summary>
        /// ��b�̃e�L�X�g�E�B���h�E�����N���X
        /// </summary>
        /// <param name="root"></param>
        /// <param name="uiName"></param>
        /// <param name="content"></param>
        /// <param name="left_percent"> �������ʂ̉��p�[�Z���g�ɂ��邩</param>
        /// <param name="top_percent"> �ォ���ʂ̉��p�[�Z���g�ɂ��邩</param>
        /// <param name="width_percent"> ��ʂ̉��p�[�Z���g�̕���</param>
        /// <param name="height_percent"> ��ʂ̉��p�[�Z���g�̍�����</param>
        public MakeConversationWindow(VisualElement root, string uiName, string content, int left_percent, int top_percent, int width_percent, int height_percent)
        {
            this.root = root;
            //UI�ɐV�����ǉ�
            this.root.Add(window);
            //Groubox�̊e�v���p�e�B�̏�����
            window.name = uiName;
            windowStyle = window.style;
            windowStyle.position = Position.Absolute;
            windowStyle.left = Screen.width * (left_percent / 100f);
            windowStyle.top = Screen.height * (top_percent / 100f);
            windowStyle.width = Screen.width * (width_percent / 100f);
            windowStyle.height = Screen.height * (height_percent / 100f);
            windowStyle.backgroundColor = new Color(.5f, .5f, .5f, .5f);
            windowStyle.borderLeftWidth = windowStyle.borderRightWidth = windowStyle.borderTopWidth = windowStyle.borderBottomWidth = 3;
            windowStyle.borderTopLeftRadius = windowStyle.borderTopRightRadius = windowStyle.borderBottomLeftRadius = windowStyle.borderBottomRightRadius = 3;
            windowStyle.borderLeftColor = windowStyle.borderRightColor = windowStyle.borderTopColor = windowStyle.borderBottomColor = Color.black;
                //UI�ɐV�����ǉ�
                window.Add(speakerNameLabel);
                //Label�̊e�v���p�e�B�̏�����
                speakerNameLabel.text = uiName;
                speakerLabStyle = speakerNameLabel.style;
                speakerLabStyle.unityTextAlign = TextAnchor.LowerLeft;
                speakerLabStyle.fontSize = 36;
                speakerLabStyle.color = Color.white;
                speakerLabStyle.position = Position.Relative;
                speakerLabStyle.backgroundColor = new Color(.3f, .3f, .3f, 1f);
                speakerLabStyle.borderLeftWidth = speakerLabStyle.borderRightWidth = speakerLabStyle.borderTopWidth = speakerLabStyle.borderBottomWidth = 3;
                speakerLabStyle.borderTopLeftRadius = speakerLabStyle.borderTopRightRadius = speakerLabStyle.borderBottomLeftRadius = speakerLabStyle.borderBottomRightRadius = 3;
                speakerLabStyle.borderLeftColor = speakerLabStyle.borderRightColor = speakerLabStyle.borderTopColor = speakerLabStyle.borderBottomColor = Color.white;                                                                                                                                                   //���x���̃C���X�^���X��                                                                                                                                                      //���x���̃C���X�^���X��
                    //UI�ɐV�����ǉ�
                    window.Add(conversationTextLabel);
                    //Label�̊e�v���p�e�B�̏�����
                    conversationTextLabel.text = content;
                    convLabStyle = conversationTextLabel.style;
                    convLabStyle.unityTextAlign = TextAnchor.MiddleLeft;
                    convLabStyle.fontSize = 24;
                    convLabStyle.color = Color.black;
                    convLabStyle.position = Position.Relative;
            //�E�B���h�E�͐������Ă��|�b�v�A�b�v���Ȃ��ƌ����Ȃ�
            window.transform.scale = Vector2.zero;
            //�X�P�[���̊e��������
            _convWindowWinWidth = window.transform.scale.x;
            _convWindowWinHeight = window.transform.scale.y;
            //�_�~�[�̏�����o�^
            WindowOpened += () => { Debug.Log("Constructor_Window-Opened"); };
            WindowClosed += () => { Debug.Log("Constructor_Window-Opened"); };
        }
        /// <summary>
        /// �Ăяo���̃^�C�~���O�ŉ�b�E�B���h�E�̃X�P�[����������
        /// </summary>
        /// <param name="scale2D"></param>
        public void SetWindowScale(Vector2 scale2D)
        {
            window.transform.scale = scale2D;
        }
        /// <summary>
        /// Update�܂���FixedUpdate���ŌĂяo�����ƁB��b�̃E�B���h�E�̃|�b�v�A�b�v����
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public void OpenWindow()
        {
            //�ȉ��E�B���h�E�g�又��
            WindowSpreadRoutine();
            SetWindowScale(new Vector2(_convWindowWinWidth, _convWindowWinHeight));
            //���ꂪ�Ăяo���ꂽ���̃f���Q�[�g
            WindowOpened();
        }
        /// <summary>
        /// Update�܂���FixedUpdate���ŌĂяo�����ƁB��b�̃E�B���h�E�̃|�b�v�A�b�v����
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public void CloseWindow()
        {
            //�ȉ��E�B���h�E�k������
            WindowCloseRoutine();
            SetWindowScale(new Vector2(_convWindowWinWidth, _convWindowWinHeight));
            //���ꂪ�Ăяo���ꂽ���̃f���Q�[�g
            WindowClosed();
        }
        /// <summary>�E�B���h�E���J���R���[�`��</summary>
        void WindowSpreadRoutine()
        { 
            while (_convWindowWinWidth < 1f)
            {
                _convWindowWinWidth += Time.deltaTime;
            }
            while (_convWindowWinHeight < 1f)
            {
                _convWindowWinHeight += Time.deltaTime;
            }
        }
        /// <summary>�E�B���h�E�����R���[�`��</summary>
        void WindowCloseRoutine()
        {
            while (_convWindowWinWidth >= 0f)
            {
                _convWindowWinWidth -= Time.deltaTime;
            }
            while(_convWindowWinHeight >= 0f)
            {
                _convWindowWinHeight -= Time.deltaTime;
            }
            Dispose();
        }
        /// <summary>
        /// �g��Ȃ��Ȃ����E�B���h�E�͔j������̂ł�����Ăяo��
        /// </summary>
        void Dispose()
        {
            //�j������̂Ńf���Q�[�g��null������Ē��g���Ȃ���
            WindowOpened = WindowClosed = () => { Debug.Log("ThisIsDiposed"); };
            window.RemoveFromHierarchy();
        }
    }
    #endregion
}