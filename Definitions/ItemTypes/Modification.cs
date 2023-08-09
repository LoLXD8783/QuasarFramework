namespace QuasarFramework.Definitions.ItemTypes
{
    public abstract class Modification : QuasarItem
    {
        public int ModificationID { get; private set; }

        public static int Count => _modifications.Count;

        public ModificationType modificationType;

        private static readonly List<Modification> _modifications = new();

        internal static int Add(Modification modification)
        {
            int count = Count;

            _modifications.Add(modification);

            return count;
        }

        internal static Modification Get(int index) => index < 0 || index >= _modifications.Count ? null : _modifications[index];

        public override void Load()
        {
            ModificationID = Add(this);

            base.Load();
        }

        public override void SetDefaults()
        {


            base.SetDefaults();
        }

        public override void Unload()
        {
            _modifications.Clear();

            base.Unload();
        }

        public enum ModificationType
        {
            Armor,
            Weapon
        }
    }
}