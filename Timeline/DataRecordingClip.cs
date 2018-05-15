using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace RecordForTimeline
{
    namespace Timeline
    {
        [Serializable]
        public class DataRecordingClip : PlayableAsset, ITimelineClipAsset
        {
            public DataRecordingBehaviour template = new DataRecordingBehaviour();

            public ClipCaps clipCaps
            {
                get { return ClipCaps.None; }
            }

            public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
            {
                var playable = ScriptPlayable<DataRecordingBehaviour>.Create(graph, template);
                return playable;
            }
        }
    }
}
