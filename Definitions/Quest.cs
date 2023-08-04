namespace QuasarFramework.Definitions
{
    public abstract class Quest : ModType
    {
        public string questName;
        public string questDescription;

        public int questStepCount;

        public bool isEventLimited;
        public bool isTimeLimited;

        public virtual void Update()
        {

        }
    }
}