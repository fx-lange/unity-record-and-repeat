using UnityEngine;
using System.Collections.Generic;

namespace TrackingRecorder
{
  public class Recording : ScriptableObject
  {
    public string recordingName = "My Recording";
    // [ReadOnly] TODO doesn't exist -> customproppertydrawer
    public float duration = 0;

    [HideInInspector]
    public List<FrameData> dataFrames = new List<FrameData>();
  }
}
