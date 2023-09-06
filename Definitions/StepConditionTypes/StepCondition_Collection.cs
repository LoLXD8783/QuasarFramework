namespace QuasarFramework.Definitions.StepConditionTypes
{
    internal class StepCondition_Collection : StepCondition
    {
        public QuasarItem itemToCollect;

        public int amount;

        public StepCondition_Collection(QuasarItem itemToCollect, int amount)
        {
            this.itemToCollect = itemToCollect;
            this.amount = amount;
        }

        public override bool StateCondition(QuasarPlayer player)
        {
            if (!player.materialInventory.ContainsKey((Material)itemToCollect))
                return false;

            return false;
        }
    }
}