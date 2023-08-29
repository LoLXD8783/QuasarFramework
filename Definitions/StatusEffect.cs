namespace QuasarFramework.Definitions
{
    public abstract class StatusEffect : ModType
    {
        public bool canStack = false;

        public bool useVanillaStackReduction = true;

        public float effectProcChance;

        public int effectStack;

        public int effectStackMaximum;

        public int effectTimeCurrent;

        public int effectTimeMaximum;

        public int ID { get; private set; }

        internal void ReapplyMe(int addativeTime = 0)
        {
            if (canStack)
            {
                effectStack++;

                if (addativeTime <= 0)
                    effectTimeCurrent = effectTimeMaximum;

                else
                    effectTimeCurrent += addativeTime;
            }

            else
            {
                if (addativeTime <= 0)
                    effectTimeCurrent = effectTimeMaximum;

                else
                    effectTimeCurrent += addativeTime;
            }
        }

        public void UpdateMe()
        {
            if (effectTimeCurrent >= effectTimeMaximum)
                effectTimeCurrent = effectStackMaximum;

            effectTimeCurrent--;

            if (effectTimeCurrent <= 0 && effectStack >= 0) 
            {
                effectStack--;
                effectTimeCurrent = effectTimeMaximum;
            }
        }

        public virtual void OnApply(QuasarNPC npc) { }
        public virtual void OnApply(QuasarPlayer player) { }

        public virtual void OnReapply(QuasarNPC npc) { }
        public virtual void OnReapply(QuasarPlayer player) { }

        public virtual void SetDefaults() { }

        public virtual void UpdateEffect(QuasarNPC npc) { }
        public virtual void UpdateEffect(QuasarPlayer player) { }
    }
}