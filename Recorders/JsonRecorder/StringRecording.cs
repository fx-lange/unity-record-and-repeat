using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace RecordForTimeline{
    public class StringRecording : Recording
    {
        [SerializeField]
        private List<StringData> myData = new List<StringData>();

        public override void Add(DataFrame data)
        {
            myData.Add((StringData)data);
        }

        protected override IEnumerable<DataFrame> GetDataFrames()
        {
            return myData.Cast<DataFrame>();
        }
    }
}