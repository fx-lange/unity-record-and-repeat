using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace RecordAndRepeat
{
    [Serializable]
    public class RecordingBehaviour : PlayableBehaviour
    {
        public RecordingBase recording;
        [HideInInspector]
        public RecordingBase watchReference = null;
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
    }
}
