namespace QuasarFramework.Definitions.ItemTypes
{
    public abstract class Armor : QuasarItem
    {
        public int[] statsArray;

        public List<Modification> augmentSlots;

        public override void OnCreated(ItemCreationContext context)
        {
            statsArray = new int[5];

            //roll stats

            augmentSlots ??= new(5);

            base.OnCreated(context);
        }

        public void UpdatePlayer(QuasarPlayer player)
        {

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