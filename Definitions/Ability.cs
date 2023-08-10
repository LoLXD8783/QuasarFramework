namespace QuasarFramework.Definitions
{
    public abstract class Ability : ModType, ILocalizedModType
    {
        public int ID { get; private set; }

        public int castCooldown;

        public int castTime;

        public int energyCost;

        public int energyOverTime;

        public string LocalizationCategory => "Ability";

        public virtual bool CanCast() => true;

        public virtual void SetDefaults() { }

        public virtual void PassiveEffect(QuasarPlayer player) { }

        public virtual void OnCast() { }

        protected sealed override void Register()
        {
            ModTypeLookup<Ability>.Register(this);

            ID = AbilityLoader.Add(this);
        }

        public sealed override void SetupContent()
        {
            SetDefaults();

            base.SetupContent();
        }

        public override string ToString() => Name;
    }
}