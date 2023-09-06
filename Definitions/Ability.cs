namespace QuasarFramework.Definitions
{
    public abstract class Ability : ModType, ILocalizedModType
    {
        public bool isActive;

        public CastType castType;

        public int castCooldownMaximum;

        public int castCooldownCurrent;

        public int castTimeMaximum;

        public int castTimeCurrent;

        public int energyCost;

        public int energyOverTime;

        public ModKeybind abilityKeybind;

        public int ID { get; private set; }

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

        public enum CastType
        {
            SINGLE,
            TOGGLE,
            CHARGE
        }
    }
}