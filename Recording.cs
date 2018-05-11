using UnityEngine;
using System.Collections.Generic;

namespace TrackingRecorder
{
    public class Recording : ScriptableObject
    {
        public string recordingName = "My Recording";
        // [ReadOnly] TODO doesn't exist -> customproppertydrawer
        public float duration = 0;

        [HideInInspector]
        public List<FrameData> dataFrames = new List<FrameData>();

        public FrameData GetFrameData(float timeS)
        {

            FrameData data = dataFrames.FindLast(x => x.time <= timeS);
            //performance: binary search would be faster O(log n) or random access via fixed fps model O(1) 
            // -> dataFrames.BinarySearch or SortedList.
            return data;
        }

        public void Log()
        {
            Debug.Log(recordingName + " " + duration + "seconds " + dataFrames.Count + " samples");
            Debug.Log(dataFrames.Count);
            dataFrames.ForEach( frame => {
               Debug.Log("time: " + frame.time + " data: " + frame.data);
            });
        }
    }
}
