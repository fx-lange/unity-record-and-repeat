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
                Recording recordingRef = recordingClip.template.recording;
                if (recordingRef)
                {
                    if (recordingClip.template.RecordingChanged())
                    {
                        clip.displayName = "Recording: " + recordingRef.name;
                        clip.duration = recordingClip.duration;
                        clip.clipIn = 0;
                    }
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