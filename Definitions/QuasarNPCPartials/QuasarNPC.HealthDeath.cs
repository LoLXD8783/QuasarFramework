namespace QuasarFramework.Definitions.QuasarNPCPartials
{
    public abstract partial class QuasarNPC : ModNPC
    {
        public Dictionary<Element, int> resistanceValues;

        public int armor;

        public int healthCurrent;

        public int healthMaximum;

        public int shieldsCurrent;

        public int shieldsMaximum;


    }
}