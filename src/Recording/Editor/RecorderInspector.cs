using UnityEngine;
using UnityEditor;
using RecordAndPlay;

[CustomEditor(typeof(Recorder), true)]
public class TimeMachineClipInspector : Editor
{
    SerializedProperty recordProp;
    SerializedProperty saveProp;

    void OnEnable()
    {
        // Setup the SerializedProperties.
        recordProp = serializedObject.FindProperty("doRecord");
        saveProp = serializedObject.FindProperty("doSave");
        // gunProp = serializedObject.FindProperty ("gun");
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
        recordProp.boolValue = !GUILayout.Toggle(!recordProp.boolValue, toggleLabel, buttonStyle, height);

        // save button
        if (GUILayout.Button("Save Recording", buttonStyle, height))
        {
            saveProp.boolValue = true;
        }

        EditorGUILayout.Space();

        serializedObject.ApplyModifiedPropertiesWithoutUndo();
    }
}