namespace QuasarFramework.Definitions.QuasarPlayerPartials
{
    public partial class QuasarPlayer
    {
        public List<StatusEffect> immuneTo = new();

        public List<StatusEffect> activeEffects = new();

        internal bool CheckImmune(StatusEffect statusEffect)
        {
            if (immuneTo.Contains(statusEffect))
                return true;

            else return false;
        }

        internal bool HasStatusEffect(StatusEffect statusEffect)
        {
            if (activeEffects.Contains(statusEffect))
                return true;

            else return false;
        }

        internal bool TryGetEffect(int index, out StatusEffect statusEffect)
        {
            if (index < 0 || index >= activeEffects.Count)
            {
                statusEffect = null;
                return false;
            }

            statusEffect = activeEffects[index];
            return true;
        }

        internal void ApplyEffect(StatusEffect effect, int timeToApply = 0)
        {
            if (CheckImmune(effect))
                return;

            if (activeEffects.Contains(effect))
            {
                int e = activeEffects.FindIndex(x => x == effect);
                activeEffects[e].ReapplyMe();
                activeEffects[e].OnReapply(this);
            }

            else
            {
                activeEffects.Insert(0, effect); //insert at 0 for most recently added.
                int e = activeEffects.FindIndex(x => x == effect);

                if (timeToApply <= 0)
                    activeEffects[e].effectTimeCurrent = activeEffects[e].effectTimeMaximum;

                else
                    activeEffects[e].effectTimeCurrent += timeToApply;
            }
        }

        internal void ClearAllEffects() => activeEffects.Clear();

        public override void PostUpdateBuffs()
        {
            foreach (StatusEffect effect in activeEffects)
            {
                effect.UpdateEffect(this);
                effect.UpdateMe();
            }

            base.PostUpdateBuffs();
        }
    }
}