using QuasarFramework.Loaders;

namespace QuasarFramework.Definitions
{
    /// <summary>
    /// A specializaton is a stand-in for a specialist system. <para></para>
    /// You can use a Specialization to store data about a specific class and their functions.
    /// </summary>
    public abstract class Specialization : ModType, ILocalizedModType
    {
        public int ID { get; private set; }

        public Ability passiveAbility;

        public int additiveArmor;
        
        public int additiveEnergy;

        public int additiveHealth;

        public int additiveShields;

        public List<Ability> abilities;

        public string LocalizationCategory => "Specialization";

        public virtual void SetDefaults() { }

        protected sealed override void Register()
        {
            ModTypeLookup<Specialization>.Register(this);

            ID = SpecializationLoader.Add(this);
        }

        public sealed override void SetupContent()
        {
            SetDefaults();

            base.SetupContent();
        }
    }
}