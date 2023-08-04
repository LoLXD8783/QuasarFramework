namespace QuasarFramework.Definitions.ItemTypes
{
    public abstract class Weapon : QuasarItem
    {
        public Archetype weaponArchetype;

        public Element weaponElement;

        public double statCritDamage;

        public float statCritChance;

        public float statStatusChance;

        public int ammoCurrent, ammoMax;

        public int magazineCurrent, magazineMax;

        public int statRange;

        public int statReload;

        public List<Modification> augmentSlots;

        public WeaponData weaponData;

        public override bool? UseItem(Player player)
        {
            if (weaponArchetype.CanUse())
                weaponArchetype.OnUse(this);

            return base.UseItem(player);
        }

        public override void OnCreated(ItemCreationContext context)
        {
            augmentSlots ??= new(4);

            base.OnCreated(context);
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
        }

        public override void UpdateInventory(Player player)
        {
            base.UpdateInventory(player);
        }
    }

    public struct WeaponData
    {

    }
}