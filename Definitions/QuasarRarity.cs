using QuasarFramework.Loaders;

namespace QuasarFramework.Definitions
{
    public abstract class QuasarRarity : ModType
    {
        /// <summary> The color this rarity will use in displays. </summary>
        public Color rarityColor;

        public int ID { get; private set; }

        /// <summary> The order in which the item this rarity is attached to will be sorted in the inventory. </summary>
        public int sortingOrder;

        /// <summary> The full name of this rarity. </summary>
        public string rarityName;

        public virtual void SetDefaults() { }

        protected sealed override void Register()
        {
            ModTypeLookup<QuasarRarity>.Register(this);

            ID = QuasarRarityLoader.Add(this);
        }

        public sealed override void SetupContent()
        {
            SetDefaults();

            base.SetupContent();
        }
    }
}