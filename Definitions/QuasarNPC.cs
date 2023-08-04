namespace QuasarFramework.Definitions
{
    public abstract class QuasarNPC : ModNPC
    {
        public Faction npcFaction;

        public int armor;

        public int healthCurrent;

        public int healthMaximum;

        public int shieldsCurrent;

        public int shieldsMaximum;

        public override bool CanHitNPC(NPC target) => false;

        public override bool CanHitPlayer(Player target, ref int cooldownSlot) => false;

        public override void SetDefaults()
        {
            NPC.noTileCollide = false;
            NPC.noGravity = false;
            base.SetDefaults();
        }
    }
}