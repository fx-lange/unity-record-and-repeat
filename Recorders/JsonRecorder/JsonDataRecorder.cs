using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RecordForTimeline
{
    public class JsonDataRecorder : Recorder
    {
        public bool createTestRecording = false;
        int samples = 120;
        int sampleIdx = 0;

        protected new void Update()
        {
            base.Update();
            if (createTestRecording)
            {
                if (sampleIdx < samples)
                {
                    RecordData(sampleIdx.ToString());
                    sampleIdx++;
                }
                else
                {
                    createTestRecording = false;
                    sampleIdx = 0;
                }
            }
        }

        protected override Recording CreateInstance()
        {
            return ScriptableObject.CreateInstance<JsonDataRecording>();
        }

        public void RecordData(string data)
        {
            JsonData opData = new JsonData();
            opData.data = data;

            RecordData(opData);
        }
    }
}
