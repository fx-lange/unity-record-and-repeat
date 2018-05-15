using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace RecordForTimeline
{
    namespace Timeline
    {
        [Serializable]
        public class DataRecordingBehaviour : PlayableBehaviour
        {
            public Recording.Recording recording;

            public override void OnPlayableCreate(Playable playable)
            {

            }
        }
    }
}
