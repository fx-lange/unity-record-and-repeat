using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.Playables;

using RecordAndPlay;

[CustomPropertyDrawer(typeof(RecordingBehaviour))]
public class RecordingBehaviourDrawer : PropertyDrawer
{
    public static bool disableRecordingSwitch = false;
    
    public override float GetPropertyHeight (SerializedProperty property, GUIContent label)
    {
        int fieldCount = 4;
        return fieldCount * EditorGUIUtility.singleLineHeight;
    }

    public override void OnGUI (Rect position, SerializedProperty property, GUIContent label)
    {
        SerializedProperty recordingProp = property.FindPropertyRelative("recording");

        Rect singleFieldRect = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);
        
        GUI.enabled = !(RecordingBehaviourDrawer.disableRecordingSwitch && Application.isPlaying);
        EditorGUI.PropertyField(singleFieldRect, recordingProp);
        GUI.enabled = true;
        
        Recording recordingRef = recordingProp.objectReferenceValue as Recording;
        if (recordingRef)
        {
            singleFieldRect.y += EditorGUIUtility.singleLineHeight;
            EditorGUI.LabelField(singleFieldRect, "Recording Name", recordingRef.name);
            
            singleFieldRect.y += EditorGUIUtility.singleLineHeight;
            EditorGUI.LabelField(singleFieldRect,"Duration", String.Format("{0:N2}",recordingRef.duration));
            
            singleFieldRect.y += EditorGUIUtility.singleLineHeight;
            EditorGUI.LabelField(singleFieldRect, "Frame Count", recordingRef.FrameCount().ToString());
        }
    }
}
