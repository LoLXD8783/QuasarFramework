namespace QuasarFramework.Loaders
{
    internal static class AbilityLoader
    {
        private static readonly List<Ability> _abilities = new();

        public static int Count => _abilities.Count;

        internal static int Add(Ability ability) 
        {
            int count = Count;

            _abilities.Add(ability);

            return count;
        }

        public static Ability Get(int index) => index < 0 || index >= _abilities.Count ? null : _abilities[index];

        internal static void Load()
        {

        }

        internal static void Unload()
        {
            _abilities.Clear();
        }
    }
}