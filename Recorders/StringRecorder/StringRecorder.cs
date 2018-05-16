using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RecordForTimeline
{
    public class StringRecorder : Recorder
    {
        protected override Recording CreateInstance()
        {
            return ScriptableObject.CreateInstance<StringRecording>();
        }

        public void RecordData(string data)
        {
            StringData opData = new StringData();
            opData.data = data;

            RecordData(opData);
        }
    }
}
