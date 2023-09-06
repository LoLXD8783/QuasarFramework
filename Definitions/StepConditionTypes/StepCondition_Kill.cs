namespace QuasarFramework.Definitions.StepConditionTypes
{
    internal class StepCondition_Kill : StepCondition
    {
        public QuasarNPC entityToKill;

        public int amount;

        public StepCondition_Kill(QuasarNPC bossToKill)
        {
            entityToKill = bossToKill;
            amount = 1;
        }

        public StepCondition_Kill(QuasarNPC entityToKill, int amount)
        {
            this.entityToKill = entityToKill;
            this.amount = amount;
        }
    }
}