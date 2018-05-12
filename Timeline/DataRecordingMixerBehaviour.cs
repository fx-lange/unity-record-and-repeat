using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace TrackingRecorder
{
    namespace Timeline
    {
        public class DataRecordingMixerBehaviour : PlayableBehaviour
        {
            // NOTE: This function is called at runtime and edit time.  Keep that in mind when setting the values of properties.
            public override void ProcessFrame(Playable playable, FrameData info, object playerData)
            {
                IDataReceiver trackBinding = playerData as IDataReceiver;

                if (trackBinding != null)
                    return;

                int inputCount = playable.GetInputCount();

                for (int i = 0; i < inputCount; i++)
                {
                    float inputWeight = playable.GetInputWeight(i);
                    ScriptPlayable<DataRecordingBehaviour> inputPlayable =
                        (ScriptPlayable<DataRecordingBehaviour>)playable.GetInput(i);
                    DataRecordingBehaviour input = inputPlayable.GetBehaviour();

                    // Use the above variables to process each frame of this playable.
                    if (inputWeight > 0 && input.recording != null)
                    {
                        float timeS = (float)inputPlayable.GetTime();
                        Recording.DataFrame dataFrame = input.recording.GetFrameData(timeS);
                        if (dataFrame != null)
                        {
                            trackBinding.ProcessData(input.recording.GetFrameData(timeS).data);
                        }
                    }
                }
            }
        }
    }
}
