namespace QuasarFramework.Loaders
{
    internal static class QRarityLoader
    {
        private static readonly List<QuasarRarity> _quasarrarities = new();

        public static int Count => _quasarrarities.Count;

        internal static int Add(QuasarRarity quasarRarity)
        {
            int count = Count;

            _quasarrarities.Add(quasarRarity);

            return count;
        }

        public static QuasarRarity Get(int index) => index < 0 || index >= _quasarrarities.Count ? null : _quasarrarities[index];

        internal static void Load() { }

        internal static void Unload()
        {
            _quasarrarities.Clear();
        }
    }
}