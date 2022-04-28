using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(OffScreenIndicatorBase), true)]
public class OffScreenIndicatorBaseEditor : Editor
{
    private OffScreenIndicator _instance;

    private void Awake()
    {
        _instance = target as OffScreenIndicator;
    }

    public override void OnInspectorGUI()
    {
        drawFieldMember();

    }

    private void drawFieldMember()
    {
        EditorGUILayout.LabelField("[Display helper]", EditorStyles.boldLabel);
        _instance.DisplayHelper = EditorGUILayout.Toggle(_instance.DisplayHelper, GUILayout.Width(16));
        if(_instance.DisplayHelper)
        {
            if(!Application.isPlaying)
                EditorGUILayout.HelpBox("Used only in play mode", MessageType.Warning);
        }
        EditorGUILayout.Space();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("[Camera]", EditorStyles.boldLabel);
        _instance.TargetCamera = EditorGUILayout.ObjectField(_instance.TargetCamera, typeof(Camera), true) as Camera;
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("[Screen Bound Offset]", EditorStyles.boldLabel);
        _instance.ScreenBoundOffset = EditorGUILayout.Slider(_instance.ScreenBoundOffset, 0.5f, 1f);
        EditorGUILayout.EndHorizontal();
    }
}
