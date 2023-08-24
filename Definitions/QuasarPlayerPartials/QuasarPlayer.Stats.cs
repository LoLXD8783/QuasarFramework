namespace QuasarFramework.Definitions.QuasarPlayerPartials
{
    public partial class QuasarPlayer
    {
        public int statAgility;

        public int statClairvoyance;

        public int statDurability;

        public int statRecuperation;

        public int statLuck;

        internal static int StatLevelCalc(int statInQuestion) => (int)Math.Floor((double)(statInQuestion / 10));
    }
}