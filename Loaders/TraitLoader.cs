namespace QuasarFramework.Loaders
{
    internal static class TraitLoader
    {
        private static readonly List<Trait> _traits = new();

        public static int Count => _traits.Count;

        internal static int Add(Trait trait)
        {
            int count = Count;

            _traits.Add(trait);

            return count;
        }

        public static Trait Get(int index) => index < 0 || index >= _traits.Count ? null : _traits[index];

        internal static void Load()
        {

        }

        internal static void Unload()
        {
            _traits.Clear();
        }
    }
}