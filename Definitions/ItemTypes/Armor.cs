namespace QuasarFramework.Definitions.ItemTypes
{
    public abstract class Armor : QuasarItem
    {
        public int[] statsArray;

        public List<Modification> augmentSlots;

        internal void RollStats()
        {

        }

        internal void UpdatePlayer(QuasarPlayer player)
        {
            player.statAgility += statsArray[0];
            player.statClairvoyance += statsArray[1];
            player.statDurability += statsArray[2];
            player.statLuck += statsArray[3];
            player.statRecuperation += statsArray[4];

            UpdateArmor(player);
        }

        public virtual void UpdateArmor(QuasarPlayer player) { }

        public override void OnCreated(ItemCreationContext context)
        {
            statsArray = new int[5];

            RollStats();

            augmentSlots ??= new(5);

            base.OnCreated(context);
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
        }

        public override void UpdateEquip(Player player)
        {
            UpdatePlayer(player.GetModPlayer<QuasarPlayer>());

            base.UpdateEquip(player);
        }

        public enum ArmorSlotType
        {
            Head,
            Chest,
            Arms,
            Legs,
            Special
        }
    }
}