using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace RecordAndPlay
{
    [Serializable]
    public class RecordingClip : PlayableAsset, ITimelineClipAsset
    {
        public RecordingBehaviour template = new RecordingBehaviour();

        public ClipCaps clipCaps
        {
            get { return ClipCaps.Looping | ClipCaps.ClipIn | ClipCaps.SpeedMultiplier; }
        }

        public override double duration
        {
            get
            {
                if (template.recording)
                {
                    return template.recording.duration;
                }
                else
                {
                    return base.duration;
                }
            }
        }

        public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
        {
            var playable = ScriptPlayable<RecordingBehaviour>.Create(graph, template);
            return playable;
        }
    }
}
