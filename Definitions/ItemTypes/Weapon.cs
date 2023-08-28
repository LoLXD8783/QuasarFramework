using static QuasarFramework.Definitions.TooltipBook;

namespace QuasarFramework.Definitions.ItemTypes
{
    public abstract class Weapon : QuasarItem
    {
        public Archetype weaponArchetype;

        public bool isReloading;

        public Element weaponElement;

        public double statCritDamage;

        public float statCritChance;

        public float statStatusChance;

        public int ammoCurrent;

        public int ammoMax;

        public int magazineCurrent;

        public int magazineMax;

        public int statRange;

        public int statReload;

        public List<Modification> augmentSlots;

        public virtual float AddedProcChance() => 0.0f;

        public void Reload()
        {
            int timer = 0;

            while (timer > 0)
            {
                isReloading = true;
                timer--;

                if (timer <= 0)
                {
                    if (magazineCurrent == 0)
                    {
                        if (ammoCurrent <= magazineMax)
                        {
                            magazineMax = ammoCurrent;
                            ammoCurrent = 0;
                        }

                        else
                        {
                            magazineCurrent += magazineMax;
                            ammoCurrent -= magazineMax;
                        }
                    }

                    else if (magazineCurrent > 0)
                    {
                        int difference = magazineMax - magazineCurrent;

                        if (difference > ammoCurrent)
                        {
                            magazineCurrent += ammoCurrent;
                            ammoCurrent = 0;
                        }
                        
                        else
                        {
                            ammoCurrent -= difference;
                            magazineCurrent = magazineMax;
                        }
                    }
                    break;
                }
            }
            QuasarFramework.WriteLogger(Mod, QuasarFramework.InputType.Debug, $"Weapon reloaded, ammo: {ammoCurrent} | mag: {magazineCurrent}");
        }

        internal float CalculateProcChance(StatusEffect effectToProc)
        {
            float totalChance = statStatusChance * effectToProc.effectProcChance; //0.5 sc and 0.5 proc chance = 0.25 total proc chance (0.5 with luck)
            totalChance += AddedProcChance();
            return totalChance;
        }

        public override bool? UseItem(Player player)
        {
            if (weaponArchetype.CanUse())
                weaponArchetype.OnUse(this);

            return base.UseItem(player);
        }

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

            var statsLine = new TooltipLine(Mod, "statsLine", statsLineText);

            //==============================================

            weaponPage.pageLines.Add(statsLine);

            base.EditTooltipBook(tooltipBook);
        }

        public override void OnCreated(ItemCreationContext context)
        {
            augmentSlots ??= new(4);

            base.OnCreated(context);
        }

        public override void UpdateInventory(Player player)
        {
            base.UpdateInventory(player);
        }
    }
}