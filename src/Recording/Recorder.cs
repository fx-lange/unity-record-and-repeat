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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

namespace RecordAndPlay
{
    public abstract class Recorder : MonoBehaviour
    {
        //folder to store recordings
        protected static string recordingsPath = "DataRecordings";

        //interface via inspector
        public bool doRecord = false;
        public bool doSave = false;

        //private members
        private Recording recording = null;
        private float startTimeSec;
        protected bool isRecording = false;
        
        protected abstract Recording CreateInstance();

        protected void Start()
        {
            doSave = false;
        }

        protected void Update()
        {
            if (!isRecording && doRecord)
            {
                StartRecording();
            }
            else if (isRecording && !doRecord)
            {
                StopRecording();
            }

            if (doSave)
            {
                StopRecording();
                SaveRecording();
                doSave = false;
            }
        }

        void StartRecording()
        {
            recording = CreateInstance();
            recording.recordingName = "recording";

            startTimeSec = Time.realtimeSinceStartup;
            isRecording = true;
        }

        void StopRecording()
        {
            doRecord = false;
            isRecording = false;
        }

        void SaveRecording()
        {
            if (recording == null || recording.duration <= 0)
            {
                return;
            }

            string path = "Assets/" + recordingsPath;
            if (!AssetDatabase.IsValidFolder(path))
            {
                AssetDatabase.CreateFolder("Assets", recordingsPath);
            }

            string assetPathAndName = AssetDatabase.GenerateUniqueAssetPath(path + "/" + recording.recordingName + ".asset");

            AssetDatabase.CreateAsset(recording, assetPathAndName);

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            recording = null;
        }
        
        protected void RecordData(DataFrame dataFrame)
        {
            if (!isRecording)
            {
                return;
            }

            dataFrame.time = Time.realtimeSinceStartup - startTimeSec;

            recording.duration = dataFrame.time; //always as long as the last data frame
            recording.Add(dataFrame);
        }
    }
}

