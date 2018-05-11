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

    void OnStart()
    {

    }

    void StartRecording()
    {
      recording = ScriptableObject.CreateInstance<Recording>();
      recording.recordingName = "UNIQUE NAME";

      startTimeSec = Time.realtimeSinceStartup;
    }

    void StopRecording()
    {

    }

    public void RecordData(string dataString)
    {
      FrameData data = new FrameData();
      data.time = Time.realtimeSinceStartup - startTimeSec;
      data.data = dataString;

      recording.duration = data.time; //always as long as the last data frame
      recording.dataFrames.Add(data);
    }

    void SaveRecording(Recording recording)
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

