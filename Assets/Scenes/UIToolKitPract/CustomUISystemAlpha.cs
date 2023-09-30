using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
#region README
//Unity UIToolKitを活用したゲーム開発を補助するものとして本スクリプトを書きました。
//UIToolKitのControlsのRadioButton,Group以外の要素なら扱えます
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
    /// UIToolKitのEnumを扱うときにインスタンス化して使う
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
        /// <summary> 値の取得 </summary>
        public Enum GetValue()
        {
            return (_enumFeild.value);
        }
        /// <summary> 可視性の設定 </summary>
        public void SetVisible(bool visible)
        {
            _enumFeild.visible = visible;
        }
        /// <summary> 可視性の取得 </summary>
        public bool GetVisible()
        {
            return (_enumFeild.visible);
        }
    }
    #endregion
    #region カスタムのUI
    /// <summary>
    /// 会話ウィンドウを扱うときにインスタンス化して使う
    /// </summary>
    public class MakeConversationWindow
    {
        //グループボックスのインスタンス化
        GroupBox window = new GroupBox();
        //プロパティ初期化の可読性のためIstyleの宣言と格納
        IStyle windowStyle;
        //話者ラベルのインスタンス化
        Label speakerNameLabel = new Label();
        //プロパティ初期化の可読性のためIstyleの宣言と格納
        IStyle speakerLabStyle;
        //会話ラベルのインスタンス化
        Label conversationTextLabel = new Label();
        //プロパティ初期化の可読性のためIstyleの宣言と格納
        IStyle convLabStyle;
        //rootをコンストラクタ外からでもいじりたいため宣言
        VisualElement root;
        //ウィンドウが開かれたときに呼び出される
        public Action WindowOpened = default;
        //ウィンドウが閉じられたとき呼び出される
        public Action WindowClosed = default;
        //会話ウィンドウの幅と高さ
        float _convWindowWinWidth, _convWindowWinHeight;
        /// <summary>
        /// 会話のテキストウィンドウ生成クラス
        /// </summary>
        /// <param name="root"></param>
        /// <param name="uiName"></param>
        /// <param name="content"></param>
        /// <param name="left_percent"> 左から画面の何パーセントにあるか</param>
        /// <param name="top_percent"> 上から画面の何パーセントにあるか</param>
        /// <param name="width_percent"> 画面の何パーセントの幅か</param>
        /// <param name="height_percent"> 画面の何パーセントの高さか</param>
        public MakeConversationWindow(VisualElement root, string uiName, string content, int left_percent, int top_percent, int width_percent, int height_percent)
        {
            this.root = root;
            //UIに新しく追加
            this.root.Add(window);
            //Grouboxの各プロパティの初期化
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
                //UIに新しく追加
                window.Add(speakerNameLabel);
                //Labelの各プロパティの初期化
                speakerNameLabel.text = uiName;
                speakerLabStyle = speakerNameLabel.style;
                speakerLabStyle.unityTextAlign = TextAnchor.LowerLeft;
                speakerLabStyle.fontSize = 36;
                speakerLabStyle.color = Color.white;
                speakerLabStyle.position = Position.Relative;
                speakerLabStyle.backgroundColor = new Color(.3f, .3f, .3f, 1f);
                speakerLabStyle.borderLeftWidth = speakerLabStyle.borderRightWidth = speakerLabStyle.borderTopWidth = speakerLabStyle.borderBottomWidth = 3;
                speakerLabStyle.borderTopLeftRadius = speakerLabStyle.borderTopRightRadius = speakerLabStyle.borderBottomLeftRadius = speakerLabStyle.borderBottomRightRadius = 3;
                speakerLabStyle.borderLeftColor = speakerLabStyle.borderRightColor = speakerLabStyle.borderTopColor = speakerLabStyle.borderBottomColor = Color.white;                                                                                                                                                   //ラベルのインスタンス化                                                                                                                                                      //ラベルのインスタンス化
                    //UIに新しく追加
                    window.Add(conversationTextLabel);
                    //Labelの各プロパティの初期化
                    conversationTextLabel.text = content;
                    convLabStyle = conversationTextLabel.style;
                    convLabStyle.unityTextAlign = TextAnchor.MiddleLeft;
                    convLabStyle.fontSize = 24;
                    convLabStyle.color = Color.black;
                    convLabStyle.position = Position.Relative;
            //ウィンドウは生成してもポップアップしないと見えない
            window.transform.scale = Vector2.zero;
            //スケールの各成分を代入
            _convWindowWinWidth = window.transform.scale.x;
            _convWindowWinHeight = window.transform.scale.y;
            //ダミーの処理を登録
            WindowOpened += () => { Debug.Log("Constructor_Window-Opened"); };
            WindowClosed += () => { Debug.Log("Constructor_Window-Opened"); };
        }
        /// <summary>
        /// 呼び出しのタイミングで会話ウィンドウのスケールを初期化
        /// </summary>
        /// <param name="scale2D"></param>
        public void SetWindowScale(Vector2 scale2D)
        {
            window.transform.scale = scale2D;
        }
        /// <summary>
        /// UpdateまたはFixedUpdate内で呼び出すこと。会話のウィンドウのポップアップ処理
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public void OpenWindow()
        {
            //以下ウィンドウ拡大処理
            WindowSpreadRoutine();
            SetWindowScale(new Vector2(_convWindowWinWidth, _convWindowWinHeight));
            //これが呼び出された時のデリゲート
            WindowOpened();
        }
        /// <summary>
        /// UpdateまたはFixedUpdate内で呼び出すこと。会話のウィンドウのポップアップ処理
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public void CloseWindow()
        {
            //以下ウィンドウ縮小処理
            WindowCloseRoutine();
            SetWindowScale(new Vector2(_convWindowWinWidth, _convWindowWinHeight));
            //これが呼び出された時のデリゲート
            WindowClosed();
        }
        /// <summary>ウィンドウを開くコルーチン</summary>
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
        /// <summary>ウィンドウを閉じるコルーチン</summary>
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
        /// 使わなくなったウィンドウは破棄するのでこれを呼び出す
        /// </summary>
        void Dispose()
        {
            //破棄するのでデリゲートはnull代入して中身をなくす
            WindowOpened = WindowClosed = () => { Debug.Log("ThisIsDiposed"); };
            window.RemoveFromHierarchy();
        }
    }
    #endregion
}