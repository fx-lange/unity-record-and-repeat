namespace RecordForTimeline
{
    [System.Serializable]
    public class JsonData : DataFrame
    {
        public string data;

        public override string ToString()
        {
            return "time: " + time + " data: " + data;
        }
    }
}
