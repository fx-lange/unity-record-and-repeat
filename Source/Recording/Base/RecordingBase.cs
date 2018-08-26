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
using System;
using System.Collections.Generic;
using System.Linq;

namespace RecordAndPlay
{
    public abstract class RecordingBase : ScriptableObject
    {
        [HideInInspector]
        public float duration = 0;

        private List<IRecord> copiedRecords = null;
        public List<IRecord> Records
        {
            get
            {
                UpdateStoredDataCopy();
                return copiedRecords;
            }
        }

        public abstract void Add(IRecord data);
        public abstract int Count();
        protected abstract IEnumerable<IRecord> GetRecords();

        public IRecord GetRecord(float timeS)
        {
            UpdateStoredDataCopy();

            IRecord data = copiedRecords.FindLast(x => x.Time <= timeS);
            return data;
        }

        public void Log()
        {
            UpdateStoredDataCopy();

            Debug.Log(String.Format("{0} - {1} seconds - {2} samples", name, duration, copiedRecords.Count));
            copiedRecords.ForEach(record => Debug.Log(record));
        }

        private void UpdateStoredDataCopy()
        {
            IEnumerable<IRecord> records = GetRecords();
            if (copiedRecords == null || copiedRecords.Count != records.Count())
            {
                copiedRecords = records.ToList();
            }
        }
    }
}