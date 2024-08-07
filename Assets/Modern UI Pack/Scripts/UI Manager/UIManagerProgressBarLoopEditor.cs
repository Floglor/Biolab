﻿using System;
using UnityEditor;
using UnityEngine.UI;
#if UNITY_EDITOR
#endif

#if UNITY_EDITOR
namespace Modern_UI_Pack.Scripts.UI_Manager
{
    [CustomEditor(typeof(UIManagerProgressBarLoop))]
    public class UIManagerProgressBarLoopEditor : Editor
    {
        override public void OnInspectorGUI()
        {
            DrawDefaultInspector();
            var UIManagerProgressBarLoop = target as UIManagerProgressBarLoop;

            using (var group = new EditorGUILayout.FadeGroupScope(Convert.ToSingle(UIManagerProgressBarLoop.hasBackground == true)))
            {
                if (group.visible == true)
                {
                    UIManagerProgressBarLoop.background = EditorGUILayout.ObjectField("Background", UIManagerProgressBarLoop.background, typeof(Image), true) as Image;
                }
            }
        }
    }
}
#endif