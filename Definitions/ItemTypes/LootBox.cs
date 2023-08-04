namespace QuasarFramework.Definitions.ItemTypes
{
    public abstract class LootBox : QuasarItem
    {
        public Dictionary<Item, int> lootPool;

        public static Item GiveReward(Dictionary<Item, int> pool)
        {
            int totalWeight = 0;

            foreach (int weight in pool.Values)
                totalWeight += weight;

            int randWeight = Main.rand.Next(0, totalWeight);

            foreach (int weight in pool.Values)
            {
                if (randWeight <= weight)
                    return pool.FirstOrDefault(x => x.Value == weight).Key;

                else
                    randWeight -= weight;
            }

            return null;
        }

        public override void SetDefaults()
        { 
            Item.maxStack = 99;

            base.SetDefaults();
        }
    }
}