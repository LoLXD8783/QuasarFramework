namespace QuasarFramework.Definitions
{
    public abstract class QuasarRarity : ModType
    {
        public Color rarityColor;

        public int ID { get; private set; }

        public int sortingOrder;

        public string rarityName;

        public virtual void SetDefaults() { }

        protected sealed override void Register()
        {
            ModTypeLookup<QuasarRarity>.Register(this);


        }

        public sealed override void SetupContent()
        {
            SetDefaults();

            base.SetupContent();
        }
    }
}