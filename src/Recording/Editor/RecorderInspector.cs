using UnityEngine;
using UnityEditor;
using RecordAndPlay;

[CustomEditor(typeof(Recorder), true)]
public class RecorderInspector : Editor
{
    SerializedProperty recordProp;
    SerializedProperty saveProp;
    SerializedProperty cancelProp;
    SerializedProperty recordingProp;
    SerializedProperty responseProp;

    bool showFeedback = false;

    GUIStyle buttonStyle;
    GUILayoutOption height;

    void OnEnable()
    {
        // Setup the SerializedProperties.
        recordProp = serializedObject.FindProperty("doRecord");
        saveProp = serializedObject.FindProperty("doSave");
        cancelProp = serializedObject.FindProperty("doCancel");
        recordingProp = serializedObject.FindProperty("recording");
        responseProp = serializedObject.FindProperty("responseText");
        
        buttonStyle = EditorStyles.miniButtonMid;
        height = GUILayout.Height(20);
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        Recorder recorder = target as Recorder;

        DrawDefaultInspector();
        EditorGUILayout.Space();

        // record toggle
        RecordToggle();

        // recording group
        if (recorder.IsRecording)
        {
            RecordingGroup();
        }

        // save button
        if (GUILayout.Button("Save Recording", buttonStyle, height))
        {
            saveProp.boolValue = true;
            showFeedback = true;
        }
        
        // cancel button
        if (GUILayout.Button("Cancel Recording", buttonStyle, height))
        {
            cancelProp.boolValue = true;
            showFeedback = true;
        }

        // feedback helpbox
        if (showFeedback)
        {
            EditorGUILayout.HelpBox(responseProp.stringValue, MessageType.Info);
        }

        EditorGUILayout.Space();

        serializedObject.ApplyModifiedPropertiesWithoutUndo();
    }

    private void RecordToggle()
    {
        Recorder recorder = target as Recorder;

        string toggleLabel;
        if (recordProp.boolValue)
        {
            toggleLabel = "Recording";
        }
        else
        {
            toggleLabel = recorder.IsPaused ? "Continue Recording" : "Record";
        }

        recordProp.boolValue = GUILayout.Toggle(recordProp.boolValue, toggleLabel, buttonStyle, height);
    }

    private void RecordingGroup()
    {
        showFeedback = false;

        Object recordingRef = recordingProp.objectReferenceValue;
        if (recordingRef)
        {
            SerializedObject recordingSO = new SerializedObject(recordingRef);
            if (recordingSO != null)
            {
                SerializedProperty nameProp = recordingSO.FindProperty("recordingName");
                EditorGUILayout.PropertyField(nameProp);

                SerializedProperty durationProp = recordingSO.FindProperty("duration");
                EditorGUILayout.LabelField("Duration", durationProp.floatValue.ToString());


                recordingSO.ApplyModifiedPropertiesWithoutUndo();

                Repaint(); //maybe not everyframe but every second?
            }
        }
    }
}