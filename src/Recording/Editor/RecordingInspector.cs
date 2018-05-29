using System;
using UnityEngine;
using UnityEditor;
using RecordAndPlay;

[CustomEditor(typeof(Recording), true)]
public class RecordingInspector : Editor
{
    GUIStyle buttonStyle;
    GUILayoutOption height;

    public override void OnInspectorGUI()
    {
        buttonStyle = EditorStyles.miniButtonMid;
        height = GUILayout.Height(20);

        serializedObject.Update();
        
        Recording recording = target as Recording;

        // EditorGUILayout.PropertyField(nameProp);
        EditorGUILayout.LabelField("Recording Name", recording.name);
        EditorGUILayout.LabelField("Duration", String.Format("{0:N2}",recording.duration));
        EditorGUILayout.LabelField("Frame Count",recording.FrameCount().ToString());
        
        // show data fields
        SerializedProperty field = serializedObject.GetIterator();
        field.NextVisible(true);
        while (field.NextVisible(false))
        {
            EditorGUILayout.PropertyField(field,true);
        }
        
        EditorGUILayout.Space();
        if (GUILayout.Button("Log Data", buttonStyle, height))
        {
            recording.Log();
        }

        serializedObject.ApplyModifiedPropertiesWithoutUndo();
    }
}