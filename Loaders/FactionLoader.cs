namespace QuasarFramework.Loaders
{
    internal static class FactionLoader
    {
        private static readonly List<Faction> _factions = new();

        public static int Count => _factions.Count;

        internal static int Add(Faction faction)
        {
            int count = Count;

            _factions.Add(faction);

            return count;
        }

        public static Faction Get(int index) => index < 0 || index >= _factions.Count ? null : _factions[index];

        internal static void Load() { }

        internal static void Unload()
        {
            _factions.Clear();
        }
    }
}