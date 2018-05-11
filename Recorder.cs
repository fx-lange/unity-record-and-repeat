using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

namespace TrackingRecorder
{

  public class Recorder : MonoBehaviour
  {

    static string recordingsPath = "DataRecordings";

    private Recording recording = null;
    private float startTimeSec;
    private bool isRecording = false;

    void Start()
    {
      StartRecording();
    }

    void StartRecording()
    {
      recording = ScriptableObject.CreateInstance<Recording>();
      recording.recordingName = "UNIQUE NAME";

      startTimeSec = Time.realtimeSinceStartup;
      isRecording = true;
    }

    void StopRecording()
    {
      isRecording = false;
    }

    public void RecordData(string dataString)
    {
      if (!isRecording)
      {
        return;
      }

      FrameData data;
      data.time = Time.realtimeSinceStartup - startTimeSec;
      data.data = dataString;

      recording.duration = data.time; //always as long as the last data frame
      recording.dataFrames.Add(data);

      Debug.Log(recording.dataFrames.Count+" "+data.time.ToString());
    }

    void SaveRecording()
    {
      string path = "Assets/" + recordingsPath;
      if (!AssetDatabase.IsValidFolder(path))
      {
        AssetDatabase.CreateFolder("Assets", recordingsPath);
      }

      string assetPathAndName = AssetDatabase.GenerateUniqueAssetPath(path + "/" + recording.recordingName + ".asset");

      AssetDatabase.CreateAsset(recording, assetPathAndName);

      AssetDatabase.SaveAssets();
      AssetDatabase.Refresh();
    }
  }

}

