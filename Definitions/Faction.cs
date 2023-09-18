using QuasarFramework.Loaders;

namespace QuasarFramework.Definitions
{
    public abstract class Faction : ModType, ILocalizedModType
    {
        public Dictionary<Element, int> elementResistances;

        public Dictionary<Faction, int> aggressiveTo;

        public int ID { get; private set; }

        public string FactionName;

        public string FactionDescription;

        public string LocalizationCategory => "Faction";

        public virtual void SetDefaults() { }

        protected sealed override void Register()
        {
            ModTypeLookup<Faction>.Register(this);

            ID = FactionLoader.Add(this);
        }

        public sealed override void SetupContent()
        {
            SetDefaults();

            base.SetupContent();
        }
    }
}