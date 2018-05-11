using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[Serializable]
public class DataRecordingBehaviour : PlayableBehaviour
{
    public TrackingRecorder.Recording recording;

    public override void OnPlayableCreate(Playable playable)
    {

    }
}
