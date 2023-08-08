namespace QuasarFramework.Definitions
{
    internal class Event
    {
        public int ID { get; private set; }

        public EventTimeSpan thisTimeSpan;

        public bool eventIsActive;

        public virtual void Update()
        {

        }
    }

    internal struct EventTimeSpan
    {
        public int startTime;
        public int endTime;

        public EventTimeSpan(int startTime, int endTime)
        {
            this.startTime = startTime;
            this.endTime = endTime;
        }
    }
}