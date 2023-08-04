namespace QuasarFramework.Definitions
{
    public abstract class Element : ModType, ILocalizedModType
    {
        public Color elementColor;

        public int ID { get; private set; }

        public string LocalizationCategory => "Element";

        public virtual void SetDefaults() { }

        protected sealed override void Register()
        {
            ModTypeLookup<Element>.Register(this);

            ID = ElementLoader.Add(this);
        }

        public override void SetupContent()
        {
            SetDefaults();

            base.SetupContent();
        }
    }
}