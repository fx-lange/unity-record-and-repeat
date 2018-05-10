using UnityEngine;
using System.Collections.Generic;

namespace TrackingRecorder
{
  //TODO probably we would never create it via menu ...
  [CreateAssetMenu(fileName = "OPRecording", menuName = "TrackingRecorder/Recording", order = 1)]
  public class Recording : ScriptableObject
  {

    public class FrameData
    {
      public float time;
      public string data;
    }

    private float startTimeSec;
    private float duration;
    private List<FrameData> dataFrames = new List<FrameData>();

    public string recName = "My Recording";

    Recording()
    {
      startTimeSec = Time.realtimeSinceStartup;
      duration = 0;
    }

    public void addData(string data)
    {
      FrameData frameData = new FrameData();
      frameData.time = Time.realtimeSinceStartup - startTimeSec;
      frameData.data = data;
      
      duration = frameData.time; //always as long as the last data frame
    }
  }
}
