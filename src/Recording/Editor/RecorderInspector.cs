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

    }

    public override void OnInspectorGUI()
    {
        buttonStyle = EditorStyles.miniButtonMid;
        height = GUILayout.Height(20);

        serializedObject.Update();

        Recorder recorder = target as Recorder;

        DrawDefaultInspector();
        EditorGUILayout.Space();

        // disable gui outside play mode
        if (!Application.isPlaying)
        {
            GUI.enabled = false;
        }

        // record toggle
        RecordToggle();

        // recording group
        if (recorder.IsRecording)
        {
            RecordingGroup();
            Repaint(); //maybe not everyframe but every second?
        }

        //only enable save/cancel during recording
        GUI.enabled = GUI.enabled && recorder.IsRecording;

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

        GUI.enabled = true;

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
            toggleLabel = recorder.IsPaused ? "Continue Recording" : "Start Recording";
        }

        recordProp.boolValue = GUILayout.Toggle(recordProp.boolValue, toggleLabel, buttonStyle, height);
    }

    private void RecordingGroup()
    {
        Recorder recorder = target as Recorder;
        
        showFeedback = false;

        Recording recordingRef = recordingProp.objectReferenceValue as Recording;
        if (recordingRef)
        {
            SerializedObject recordingSO = new SerializedObject(recordingRef);
            if (recordingSO != null)
            {
                EditorGUILayout.LabelField("Destination Folder",recorder.DestinationFolder);
                
                SerializedProperty nameProp = recordingSO.FindProperty("recordingName");
                EditorGUILayout.PropertyField(nameProp);

                SerializedProperty durationProp = recordingSO.FindProperty("duration");
                EditorGUILayout.LabelField("Duration", durationProp.floatValue.ToString());
                
                EditorGUILayout.LabelField("Frame Count", recordingRef.FrameCount().ToString());

                recordingSO.ApplyModifiedPropertiesWithoutUndo();
            }
        }
    }
}