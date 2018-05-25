using UnityEngine;
using UnityEditor;
using RecordAndPlay;

[CustomEditor(typeof(Recorder), true)]
public class RecorderInspector : Editor
{
    SerializedProperty recordProp;
    SerializedProperty saveProp;
    SerializedProperty recordingProp;

    void OnEnable()
    {
        // Setup the SerializedProperties.
        recordProp = serializedObject.FindProperty("doRecord");
        saveProp = serializedObject.FindProperty("doSave");
        recordingProp = serializedObject.FindProperty("recording");
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        serializedObject.Update();


        EditorGUILayout.Space();

        GUIStyle buttonStyle = EditorStyles.miniButtonMid;
        GUILayoutOption height = GUILayout.Height(20);

        // record toggle
        string toggleLabel = recordProp.boolValue ? "Recording" : "Record";
        recordProp.boolValue = GUILayout.Toggle(recordProp.boolValue, toggleLabel, buttonStyle, height);

        if (recordProp.boolValue)
        {
            Object recordingRef = recordingProp.objectReferenceValue;
            if(recordingRef){
                SerializedObject soSO = new SerializedObject(recordingRef);
                if (soSO != null)
                {
                    SerializedProperty nameProp = soSO.FindProperty("recordingName");
                    EditorGUILayout.PropertyField(nameProp);
                    
                    SerializedProperty durationProp = soSO.FindProperty("duration");
                    EditorGUILayout.LabelField("Duration",durationProp.floatValue.ToString());
                    
                    soSO.ApplyModifiedPropertiesWithoutUndo();
                    
                    Repaint(); //maybe not everyframe but every second?
                }
            }
        }

        // save button
        if (GUILayout.Button("Save Recording", buttonStyle, height))
        {
            saveProp.boolValue = true;
        }

        EditorGUILayout.Space();

        serializedObject.ApplyModifiedPropertiesWithoutUndo();
    }
}