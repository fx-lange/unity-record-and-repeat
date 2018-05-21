// MIT License

// Copyright (c) 2018 Felix Lange 

// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:

// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.

// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace RecordAndPlay
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