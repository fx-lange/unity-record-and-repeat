using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace RecordForTimeline{
    public class JsonDataRecording : Recording
    {
        [SerializeField]
        private List<JsonData> myData = new List<JsonData>();

        public override void Add(DataFrame data)
        {
            myData.Add((JsonData)data);
        }

        protected override IEnumerable<DataFrame> GetDataFrames()
        {
            return myData.Cast<DataFrame>();
        }
    }
}