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
        [HideInInspector]
        public Recording watchReference = null;
        public bool RecordingChanged()
        {
            if (watchReference == recording)
            {
                return false;
            }
            else
            {
                watchReference = recording;
                return true;
            }
        }

        public void CleanClone(RecordingBehaviour other)
        {
            watchReference = other.watchReference;
        }

        // public override void OnGraphStart(Playable playable)
        // {
        //     Debug.LogWarning("RecordingBehaviour::OnGraphStart");
        // }

        // public override void OnPlayableCreate(Playable playable)
        // {
        //     Debug.LogWarning("RecordingBehaviour::OnPlayableCreate");
        // }

        // public override void PrepareData(Playable playable, FrameData info)
        // {
        //     Debug.LogWarning("RecordingBehaviour::PrepareData");
        // }

        // public override void PrepareFrame(Playable playable, FrameData info)
        // {
        //     Debug.LogWarning("RecordingBehaviour::PrepareFrame");
        // }

        // public override void ProcessFrame(Playable playable, FrameData info, object playerData)
        // {
        //     Debug.LogWarning("RecordingBehaviour::ProcessFrame");
        // }
    }
}
