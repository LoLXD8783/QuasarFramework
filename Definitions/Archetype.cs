namespace QuasarFramework.Definitions
{
    public abstract class Archetype : ModType
    {
        private int useCooldownTimer;

        public bool isMelee;

        public bool isRanged;

        public int ID { get; private set; }

        public int statHeavyAttackDamage;

        public int statRange;

        public int statReloadSpeed;

        /// <summary>
        /// Allows you to determine when a weapon with this archetype can be used.
        /// </summary>
        /// <returns></returns>
        public virtual bool CanUse() => true;

        /// <summary>
        /// Allows you to make things happen when a weapon with this Archetype is used.
        /// </summary>
        public virtual void OnUse(Weapon weapon) { }

        public virtual void SetDefaults() { }

        protected sealed override void Register()
        {
            ModTypeLookup<Archetype>.Register(this);

            ID = ArchetypeLoader.Add(this);
        }

        public sealed override void SetupContent()
        {
            SetDefaults();

            base.SetupContent();
        }
    }
}