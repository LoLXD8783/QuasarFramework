namespace QuasarFramework.Loaders
{
    internal static class SpecializationLoader
    {
        private static readonly List<Specialization> _specializations = new();

        public static int Count => _specializations.Count;

        internal static int Add(Specialization specialization)
        {
            int count = Count;

            _specializations.Add(specialization);

            return count;
        }

        public static Specialization Get(int index) => index < 0 || index >= _specializations.Count ? null : _specializations[index];

        internal static void Load()
        {

        }

        internal static void Unload()
        {
            _specializations.Clear();
        }
    }
}