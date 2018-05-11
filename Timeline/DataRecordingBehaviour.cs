using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace TrackingRecorder
{
    namespace Timeline
    {
        [Serializable]
        public class DataRecordingBehaviour : PlayableBehaviour
        {
            public Recording.DataRecording recording;

            public override void OnPlayableCreate(Playable playable)
            {

            }
        }
    }
}
