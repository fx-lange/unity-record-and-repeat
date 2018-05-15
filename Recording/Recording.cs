using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace RecordForTimeline.Recording
{
    public abstract class Recording : ScriptableObject
    {
        public string recordingName = "My Recording";
        // [ReadOnly] TODO doesn't exist -> customproppertydrawer
        public float duration = 0;

        private List<DataFrame> copiedDataFrames = null;

        protected abstract IEnumerable<DataFrame> GetDataFrames();
        public abstract void Add(DataFrame data);

        public DataFrame GetFrameData(float timeS)
        {
            UpdateStoredDataCopy(); //TODO probably not here but in a "prepare" method?

            DataFrame data = copiedDataFrames.FindLast(x => x.time <= timeS);
            /* TODO performance: 
            binary search would be faster O(log n) (or random access via fixed fps model O(1)) 
            -> dataFrames.BinarySearch or SortedList.
            */
            return data;
        }

        public void Log()
        {
            UpdateStoredDataCopy();

            Debug.Log(recordingName + " " + duration + "seconds " + copiedDataFrames.Count + " samples");
            copiedDataFrames.ForEach(frame => Debug.Log(frame));
        }

        private void UpdateStoredDataCopy()
        {
            IEnumerable<DataFrame> frames = GetDataFrames();
            if (copiedDataFrames == null || copiedDataFrames.Count != frames.Count())
            {
                copiedDataFrames = frames.ToList();
            }
        }
    }
}