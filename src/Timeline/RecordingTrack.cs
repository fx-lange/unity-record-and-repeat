using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace RecordAndPlay
{
    [TrackColor(0.855f, 0.8623f, 0.87f)]
    [TrackClipType(typeof(RecordingClip))]
    [TrackBindingType(typeof(DataListener))]
    public class RecordingTrack : TrackAsset
    {
        public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
        {
            foreach (var clip in GetClips())
            {
                RecordingClip recordingClip = clip.asset as RecordingClip;
                Recording recording = recordingClip.template.recording;
                if (recording)
                {
                    clip.displayName = "Recording: " + recording.recordingName;
                }
                else
                {
                    clip.displayName = "...";
                }
            }

            return ScriptPlayable<RecordingMixerBehaviour>.Create(graph, inputCount);
        }
    }
}