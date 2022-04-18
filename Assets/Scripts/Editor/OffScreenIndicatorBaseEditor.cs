using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(OffScreenIndicatorBase), true)]
public class OffScreenIndicatorBaseEditor : Editor
{
    private OffScreenIndicator instance;

    private void Awake()
    {
        instance = target as OffScreenIndicator;
    }

    public override void OnInspectorGUI()
    {
        DrawFieldMember();

    }

    private void DrawFieldMember()
    {
        EditorGUILayout.LabelField("[Display helper]", EditorStyles.boldLabel);
        instance.DisplayHelper = EditorGUILayout.Toggle(instance.DisplayHelper, GUILayout.Width(16));
        if(instance.DisplayHelper)
        {
            if(!Application.isPlaying)
                EditorGUILayout.HelpBox("Used only in play mode", MessageType.Warning);
        }
        EditorGUILayout.Space();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("[Camera]", EditorStyles.boldLabel);
        instance.TargetCamera = EditorGUILayout.ObjectField(instance.TargetCamera, typeof(Camera), true) as Camera;
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("[Screen Bound Offset]", EditorStyles.boldLabel);
        instance.ScreenBoundOffset = EditorGUILayout.Slider(instance.ScreenBoundOffset, 0.5f, 1f);
        EditorGUILayout.EndHorizontal();
    }
}
