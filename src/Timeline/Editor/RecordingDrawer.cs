using UnityEditor;
using UnityEngine;
using UnityEngine.Playables;

using RecordAndPlay;

[CustomPropertyDrawer(typeof(RecordingBehaviour))]
public class RecordingDrawer : PropertyDrawer
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
        // SerializedProperty intensityProp = property.FindPropertyRelative("intensity");
        // SerializedProperty bounceIntensityProp = property.FindPropertyRelative("bounceIntensity");
        // SerializedProperty rangeProp = property.FindPropertyRelative("range");

        Rect singleFieldRect = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);
        
        GUI.enabled = !(RecordingDrawer.disableRecordingSwitch && Application.isPlaying);
        EditorGUI.PropertyField(singleFieldRect, recordingProp);
        GUI.enabled = true;
        
        Recording recordingRef = recordingProp.objectReferenceValue as Recording;
        if (recordingRef)
        {
            SerializedObject recordingSO = new SerializedObject(recordingRef);
            if (recordingSO != null)
            {
                SerializedProperty nameProp = recordingSO.FindProperty("recordingName");
                // EditorGUILayout.PropertyField(nameProp);
                singleFieldRect.y += EditorGUIUtility.singleLineHeight;
                EditorGUI.PropertyField(singleFieldRect, nameProp);
                

                SerializedProperty durationProp = recordingSO.FindProperty("duration");
                // EditorGUILayout.LabelField("Duration", durationProp.floatValue.ToString());
                singleFieldRect.y += EditorGUIUtility.singleLineHeight;
                EditorGUI.LabelField(singleFieldRect,"Duration", durationProp.floatValue.ToString());
                
                
                // EditorGUILayout.LabelField("Frame Count", recordingRef.FrameCount().ToString());
                singleFieldRect.y += EditorGUIUtility.singleLineHeight;
                EditorGUI.LabelField(singleFieldRect, "Frame Count", recordingRef.FrameCount().ToString());

                recordingSO.ApplyModifiedPropertiesWithoutUndo();
            }
        }
    }
}
