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

    void Start()
    {
      recording = ScriptableObject.CreateInstance<Recording>();
      recording.recName = "UNIQUE NAME";
      SaveRecording(recording);
    }

    void StartRecording()
    {

    }

    void StopRecording()
    {

    }
		
		void RecordData(string data){
			recording.addData(data);
		}

    static void SaveRecording(Recording recording)
    {
      string path = "Assets/" + recordingsPath;
      if (!AssetDatabase.IsValidFolder(path))
      {
        AssetDatabase.CreateFolder("Assets", recordingsPath);
      }

      string assetPathAndName = AssetDatabase.GenerateUniqueAssetPath(path + "/" + recording.recName + ".asset");

      AssetDatabase.CreateAsset(recording, assetPathAndName);

      AssetDatabase.SaveAssets();
      AssetDatabase.Refresh();
    }
  }

}

