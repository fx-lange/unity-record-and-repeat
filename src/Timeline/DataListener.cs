using UnityEngine;

namespace RecordAndPlay
{
    public abstract class DataListener : MonoBehaviour
    {
        public abstract void ProcessData(DataFrame data);
    }
}
