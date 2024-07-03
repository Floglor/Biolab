﻿using System;
using TMPro;
using UnityEditor;
#if UNITY_EDITOR
#endif

#if UNITY_EDITOR
namespace Modern_UI_Pack.Scripts.UI_Manager
{
    [CustomEditor(typeof(UIManagerSlider))]
    public class UIManagerSliderEditor : Editor
    {
        override public void OnInspectorGUI()
        {
            DrawDefaultInspector();
            var UIManagerSlider = target as UIManagerSlider;

            using (var group = new EditorGUILayout.FadeGroupScope(Convert.ToSingle(UIManagerSlider.hasLabel == true)))
            {
                if (group.visible == true)
                    UIManagerSlider.label = EditorGUILayout.ObjectField("Label", UIManagerSlider.label, typeof(TextMeshProUGUI), true) as TextMeshProUGUI;
            }

            using (var group = new EditorGUILayout.FadeGroupScope(Convert.ToSingle(UIManagerSlider.hasPopupLabel == true)))
            {
                if (group.visible == true)
                    UIManagerSlider.popupLabel = EditorGUILayout.ObjectField("Popup Label", UIManagerSlider.popupLabel, typeof(TextMeshProUGUI), true) as TextMeshProUGUI;
            }
        }
    }
}
#endif