using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace RecordForTimeline
{
    namespace Timeline
    {
        [TrackColor(0.855f, 0.8623f, 0.87f)]
        [TrackClipType(typeof(DataRecordingClip))]
        [TrackBindingType(typeof(OpenPoseDrawer))]
        public class DataRecordingTrack : TrackAsset
        {
            public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
            {
                return ScriptPlayable<DataRecordingMixerBehaviour>.Create(graph, inputCount);
            }
        }
    }
}
