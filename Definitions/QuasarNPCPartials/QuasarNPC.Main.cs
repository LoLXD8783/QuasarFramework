namespace QuasarFramework.Definitions.QuasarNPCPartials
{
    public abstract partial class QuasarNPC : ModNPC
    {
        public Faction npcFaction;

        public override bool CanHitNPC(NPC target) => false;

        public override bool CanHitPlayer(Player target, ref int cooldownSlot) => false;


    }
}