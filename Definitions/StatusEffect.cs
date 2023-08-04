namespace QuasarFramework.Definitions
{
    public abstract class StatusEffect : ModBuff
    {
        public float effectStackMultiplier;

        public int thisBuffTime;

        public int effectStack;

        public override bool ReApply(NPC npc, int time, int buffIndex)
        {
            return base.ReApply(npc, time, buffIndex);
        }

        public override bool ReApply(Player player, int time, int buffIndex)
        {
            effectStack++;
            player.buffTime[this.Type] = thisBuffTime;

            return base.ReApply(player, time, buffIndex);
        }

        public override bool RightClick(int buffIndex) => false;

        public override void PostDraw(SpriteBatch spriteBatch, int buffIndex, BuffDrawParams drawParams)
        {
            Utils.DrawBorderString(spriteBatch, effectStack.ToString(), drawParams.Position + Vector2.One, Color.White, 0.75f);
            //draw stack value.

            base.PostDraw(spriteBatch, buffIndex, drawParams);
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            base.Update(npc, ref buffIndex);
        }

        public override void Update(Player player, ref int buffIndex)
        {
            if (player.buffTime[this.Type] <= 0)
            {
                effectStack--; //reduce our stack
                player.buffTime[this.Type] = thisBuffTime; //reset the timer
            }



            base.Update(player, ref buffIndex);
        }
    }
}