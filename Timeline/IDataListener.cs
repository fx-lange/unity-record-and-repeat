namespace TrackingRecorder
{
    namespace Timeline
    {
        public interface IDataListener
        {
            void ProcessData(Recording.DataFrame data);
        }
    }
}
