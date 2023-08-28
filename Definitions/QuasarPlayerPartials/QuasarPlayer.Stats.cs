namespace QuasarFramework.Definitions.QuasarPlayerPartials
{
    public partial class QuasarPlayer
    {
        public int luckRollCount;

        public int statAgility;

        public int statClairvoyance;

        public int statDurability;

        public int statRecuperation;

        public int statLuck;

        internal static int StatLevelCalc(int statInQuestion) => (int)Math.Floor((double)(statInQuestion / 10));

        internal void AddedDamageResistance(int durability) { }

        internal void BaseEnergyRegeneration(int clairvoyance) { }

        internal void BaseHealthRegen(int recuperation) { }

        internal void BaseMovementSpeeds(int agility) { }

        internal int GetLuckRollCount()
        {
            int luckLevel = StatLevelCalc(statLuck);

            return 0;
        }
    }
}