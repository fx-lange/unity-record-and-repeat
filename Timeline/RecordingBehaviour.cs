using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace RecordAndPlay
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
