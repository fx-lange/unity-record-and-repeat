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
using UnityEditor;
using UnityEngine;
using UnityEngine.Playables;

using RecordAndPlay;

[CustomPropertyDrawer(typeof(RecordingBehaviour))]
public class RecordingBehaviourDrawer : PropertyDrawer
{
    public static bool disableRecordingSwitch = true;
    
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
