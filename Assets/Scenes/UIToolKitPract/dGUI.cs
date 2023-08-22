using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dGUI : MonoBehaviour
{
    /// <summary> ��ʂ̒��SX���� </summary>
    float cx = Screen.width / 2.0f;
    /// <summary> ��ʂ̒��SY���� </summary>
    float cy = Screen.height / 2.0f;
    /// <summary> GUI�̊�{�I�ȃI�v�V�����Z�b�e�B���O </summary>
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
