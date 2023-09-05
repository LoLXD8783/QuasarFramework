namespace QuasarFramework.Definitions
{
    public abstract class Quest : ModType
    {
        /// <summary> Whether or not this quest is attached to another quest. </summary>
        public bool isAdjacent;

        /// <summary> Whether or not this quest is timegated by a serverside Event. </summary>
        public bool isEventLimited;

        /// <summary> Whether or not this quest is "important", and cannot be deleted. </summary>
        public bool isImportant;

        /// <summary> Whether or not this quest is in-general timegated serverside. </summary>
        public bool isTimeLimited;

        public int ID { get; private set; }

        public int StepCount => QuestSteps.Count;

        public List<string> QuestSteps;

        /// <summary> The visual name of this quest. </summary>
        public string questName;

        /// <summary> The visual description of this quest. </summary>
        public string questDescription;

        public virtual void Update()
        {

        }

        public virtual void SetDefaults() { }

        protected sealed override void Register()
        {
            ModTypeLookup<Quest>.Register(this);


        }

        public sealed override void SetupContent()
        {
            SetDefaults();

            base.SetupContent();
        }
    }
}