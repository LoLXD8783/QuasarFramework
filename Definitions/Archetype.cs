namespace QuasarFramework.Definitions
{
    public abstract class Archetype : ModType
    {
        public int ID { get; private set; }

        private int useCooldownTimer;

        public bool isMelee;

        public bool isRanged;

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

            //ID = ...
        }

        public sealed override void SetupContent()
        {
            SetDefaults();

            base.SetupContent();
        }
    }
}