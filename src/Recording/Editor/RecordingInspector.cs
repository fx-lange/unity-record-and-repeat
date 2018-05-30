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

[CustomEditor(typeof(Recording), true)]
public class RecordingInspector : Editor
{
    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        Recording recording = target as Recording;

        EditorGUILayout.BeginVertical(EditorStyles.helpBox);

        // recording details as labels
        EditorGUILayout.LabelField("Recording Name", recording.name);
        EditorGUILayout.LabelField("Type",recording.GetType().Name);
        EditorGUILayout.LabelField("Duration", String.Format("{0:N2}", recording.duration));
        EditorGUILayout.LabelField("Frame Count", recording.FrameCount().ToString());

        EditorGUILayout.EndVertical();
        EditorGUILayout.Space();

        // button to log all data frames
        if (GUILayout.Button("Log Data Frames", EditorStyles.miniButtonMid, GUILayout.Height(20)))
        {
            recording.Log();
        }
        
        EditorGUILayout.Space();

        // show data fields
        SerializedProperty field = serializedObject.GetIterator();
        field.NextVisible(true);
        while (field.NextVisible(false))
        {
            EditorGUILayout.PropertyField(field, true);
        }

        serializedObject.ApplyModifiedPropertiesWithoutUndo();
    }
}