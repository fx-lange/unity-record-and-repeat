using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace RecordAndPlay
{
    public class RecordingMixerBehaviour : PlayableBehaviour
    {
        // NOTE: This function is called at runtime and edit time.  Keep that in mind when setting the values of properties.
        public override void ProcessFrame(Playable playable, FrameData info, object playerData)
        {
            DataListener trackBinding = playerData as DataListener;

            if (trackBinding == null)
            {
                Debug.LogWarning("Track without Trackbinding");
                return;
            }

            int inputCount = playable.GetInputCount();

            for (int i = 0; i < inputCount; i++)
            {
                float inputWeight = playable.GetInputWeight(i);
                ScriptPlayable<RecordingBehaviour> inputPlayable =
                    (ScriptPlayable<RecordingBehaviour>)playable.GetInput(i);
                RecordingBehaviour input = inputPlayable.GetBehaviour();

                // Use the above variables to process each frame of this playable.
                if (inputWeight > 0 && input.recording != null)
                {
                    float timeS = (float)inputPlayable.GetTime();
                    float duration = input.recording.duration;
                    if (duration > 0)
                    {
                        timeS = timeS % duration; //loop
                    }
                    
                    DataFrame dataFrame = input.recording.GetFrameData(timeS);
                    if (dataFrame != null)
                    {
                        trackBinding.ProcessData(dataFrame);
                    }
                }
            }
        }
    }
}