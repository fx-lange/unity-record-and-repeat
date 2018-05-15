using UnityEngine;

namespace RecordForTimeline
{
    namespace Timeline
    {
        public abstract class DataListener : MonoBehaviour
        {
            public abstract void ProcessData(Recording.DataFrame data);
        }
    }
}
