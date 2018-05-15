using UnityEngine;

namespace RecordForTimeline.Timeline
{
    public abstract class DataListener : MonoBehaviour
    {
        public abstract void ProcessData(Recording.DataFrame data);
    }
}
