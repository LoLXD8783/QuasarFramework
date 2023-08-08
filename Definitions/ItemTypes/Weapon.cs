using static QuasarFramework.Definitions.TooltipBook;

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

        public override void EditTooltipBook(TooltipBook tooltipBook)
        {
            TooltipPage weaponPage = new();

            weaponPage.pageTitle = "Weapon Stats";

            string statsLineText =
                $"CRITICAL CHANCE | {statCritChance}% \n" +
                $"CRITICAL DAMAGE | {statCritDamage}x \n" +
                $"STATUS CHANCE | {statStatusChance}% \n";

            if (weaponArchetype.isRanged)
                statsLineText += 
                    $"RANGE | {statRange} \n" +
                    $"RELOAD SPEED | {statReload} \n";

            var statsLine = new TooltipLine(Mod, "statsLine",
                $"CRITICAL CHANCE | {statCritChance}% \n" +
                $"CRITICAL DAMAGE | {statCritDamage}x \n" +
                $"STATUS CHANCE | {statStatusChance}% \n");

            //==============================================

            weaponPage.pageLines.Add(statsLine);

            base.EditTooltipBook(tooltipBook);
        }

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