using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dGUI : MonoBehaviour
{
    /// <summary> 画面の中心X成分 </summary>
    float cx = Screen.width / 2.0f;
    /// <summary> 画面の中心Y成分 </summary>
    float cy = Screen.height / 2.0f;
    /// <summary> GUIの基本的なオプションセッティング </summary>
    GUIStyle _defaultStyle = new GUIStyle();

    float w = 500, h = 250;
    string[] _tooltips = { "a", "b", "c" };
    int _selectedToolbarButton = 0;
    float _sliderValue;
    Vector2 _sliderPosition;
    private void OnGUI()
    {
        _defaultStyle.normal.textColor = Color.cyan;
        _defaultStyle.alignment = TextAnchor.MiddleCenter;
        _defaultStyle.fontSize = 36;
        GUILayout.BeginArea(new Rect(cx, cy, w,h), _defaultStyle);
        _selectedToolbarButton = GUILayout.Toolbar(_selectedToolbarButton, _tooltips);
        //_sliderValue = GUILayout.VerticalSlider(_sliderValue, 0f, 100f);
        _sliderValue = GUILayout.VerticalScrollbar(_sliderValue, 1f, 0f, 100f);
        GUILayout.Space(w);
        GUILayout.EndArea();
    }
}
