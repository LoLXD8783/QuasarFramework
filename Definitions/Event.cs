namespace QuasarFramework.Definitions
{
    internal class Event
    {
        public int ID { get; private set; }

        public EventTimeSpan thisTimeSpan;

        public bool isActive;

        public virtual void OnEndEvent() { }

        public virtual void OnStartEvent() { }

        public virtual void UpdateEvent() { }
    }

    internal struct EventTimeSpan
    {
        public int startTime;
        public int endTime;

        public DateTime startTimeDated;
        public DateTime endTimeDated;

        public EventTimeSpan(int startTime, int endTime)
        {
            this.startTime = startTime;
            this.endTime = endTime;
        }

        public EventTimeSpan(DateTime startTime, DateTime endTime)
        {
            startTimeDated = startTime;
            endTimeDated = endTime;
        }
    }
}