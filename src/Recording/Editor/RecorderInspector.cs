// MIT License

// Copyright (c) 2018 Felix Lange 

// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:

// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.

// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

using System;
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
    SerializedProperty recordingNameProp;

    bool showFeedback = false;

    GUIStyle buttonStyle;
    GUILayoutOption height;

    void OnEnable()
    {
        Recorder recorder = target as Recorder;
        recorder.InitRecording();

        // Setup the SerializedProperties.
        recordProp = serializedObject.FindProperty("doRecord");
        saveProp = serializedObject.FindProperty("doSave");
        cancelProp = serializedObject.FindProperty("doCancel");
        recordingProp = serializedObject.FindProperty("recording");
        responseProp = serializedObject.FindProperty("responseText");
        recordingNameProp = serializedObject.FindProperty("recordingName");
    }

    public override void OnInspectorGUI()
    {
        Recorder recorder = target as Recorder;
        serializedObject.Update();

        buttonStyle = EditorStyles.miniButtonMid;
        height = GUILayout.Height(20);

        DrawDefaultInspector();
        EditorGUILayout.Space();

        // disable gui outside play mode
        if (recorder.disableIfNotPlaying && !Application.isPlaying)
        {
            EditorGUILayout.HelpBox("For this Recorder recording is disabled while Application is not playing.", MessageType.Info);
            GUI.enabled = false;
        }

        // record toggle
        RecordToggle();

        // recording group
        RecordingGroup();

        if (recorder.IsRecording)
        {
            showFeedback = false;

            Repaint(); // drawn 10 times per second
        }

        //only enable save/cancel if recording has been started
        GUI.enabled = GUI.enabled && recorder.IsRecordingStarted;

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

        Color defaultColor = GUI.backgroundColor;
        string toggleLabel;
        if (recordProp.boolValue)
        {
            GUI.backgroundColor = Color.red;
            toggleLabel = "Recording";
        }
        else
        {
            toggleLabel = recorder.IsPaused ? "Continue Recording" : "Start Recording";
        }

        recordProp.boolValue = GUILayout.Toggle(recordProp.boolValue, toggleLabel, buttonStyle, height);

        //reset background color
        GUI.backgroundColor = defaultColor;
    }

    private void RecordingGroup()
    {
        Recorder recorder = target as Recorder;

        Recording recordingRef = recordingProp.objectReferenceValue as Recording;

        string type = "-";
        string duration = "-";
        string frameCount = "0";

        if (recordingRef)
        {
            type = recordingRef.GetType().Name;
            duration = String.Format("{0:N2}", recordingRef.duration);
            frameCount = recordingRef.FrameCount().ToString();
        }

        EditorGUILayout.BeginVertical(EditorStyles.helpBox);
        EditorGUILayout.PropertyField(recordingNameProp);

        EditorGUILayout.LabelField("Type", type);
        EditorGUILayout.LabelField("Duration", duration);
        EditorGUILayout.LabelField("Frame Count", frameCount);

        EditorGUILayout.LabelField("Destination Folder", recorder.DestinationFolder);

        EditorGUILayout.EndVertical();
    }
}