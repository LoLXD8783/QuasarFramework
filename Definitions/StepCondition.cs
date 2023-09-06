namespace QuasarFramework.Definitions
{
    internal abstract class StepCondition
    {
        public bool isConditionCompleted;

        public string conditionDescription;

        public virtual bool StateCondition(QuasarPlayer player) => false;

        public virtual void SetDefaults() { }

        public virtual void UpdateCondition() { }
    }
}