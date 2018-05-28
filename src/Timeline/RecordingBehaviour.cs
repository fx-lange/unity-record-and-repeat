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

        private Recording watchReference = null;
        public bool RecordingChanged()
        {
            if (watchReference == recording)
            {
                Debug.LogWarning("RecordingChanged NOT");
                return false;
            }
            else
            {
                Debug.LogWarning("RecordingChanged YES");
                watchReference = recording;
                return true;
            }
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
