namespace QuasarFramework.Definitions
{
    public abstract class Ability : ModType, ILocalizedModType
    {
        public bool isActive;

        public int castCooldownMaximum;

        public int castCooldownCurrent;

        public int castTimeMaximum;

        public int castTimeCurrent;

        public int energyCost;

        public int energyOverTime;

        public int ID { get; private set; }

        public ModKeybind abilityKeybind;

        public string LocalizationCategory => "Ability";

        public void CastMe()
        {
            if (CanCast())
            {
                OnCastType();

                OnCast();
            }
        }

        public virtual bool CanCast() => true;

        public virtual void SetDefaults() { }

        public virtual void PassiveEffect(QuasarPlayer player) { }

        public virtual void OnCast() { } //custom cast parameters by ability

        protected virtual void OnCastType() { } //custom cast parameters by type of ability

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