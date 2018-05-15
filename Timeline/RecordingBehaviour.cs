using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace RecordForTimeline
{
    [Serializable]
    public class RecordingBehaviour : PlayableBehaviour
    {
        public Recording recording;

        public override void OnPlayableCreate(Playable playable)
        {

        }
    }
}
