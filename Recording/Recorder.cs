using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

namespace TrackingRecorder
{
    namespace Recording
    {
        public class Recorder : MonoBehaviour
        {

            static string recordingsPath = "DataRecordings";

            public bool doRecord = false;
            public bool doSave = false;

            private DataRecording recording = null;
            private float startTimeSec;
            private bool isRecording = false;

            void Start()
            {
                doSave = false;
            }

            void Update()
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
                recording = ScriptableObject.CreateInstance<DataRecording>();
                recording.recordingName = "UNIQUE NAME";

                startTimeSec = Time.realtimeSinceStartup;
                isRecording = true;
            }

            void StopRecording()
            {
                doRecord = false;
                isRecording = false;
            }

            public void RecordData(string dataString)
            {
                if (!isRecording)
                {
                    return;
                }

                FrameData data = new FrameData();
                data.time = Time.realtimeSinceStartup - startTimeSec;
                data.data = dataString;

                recording.duration = data.time; //always as long as the last data frame
                recording.dataFrames.Add(data);

                Debug.Log(recording.dataFrames.Count + " " + data.time.ToString());
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
        }
    }

}

