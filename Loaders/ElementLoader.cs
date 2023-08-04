namespace QuasarFramework.Loaders
{
    internal static class ElementLoader
    {
        private static readonly List<Element> _elements = new();

        public static int Count => _elements.Count;

        internal static int Add(Element element)
        {
            int count = Count;

            _elements.Add(element);

            return count;
        }

        public static Element Get(int index) => index < 0 || index >= _elements.Count ? null : _elements[index];

        internal static void Load()
        {

        }

        internal static void Unload()
        {
            _elements.Clear();
        }
    }
}
