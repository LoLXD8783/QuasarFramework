namespace QuasarFramework.Loaders
{
    public abstract class QuasarLoader<T> where T : ModType
    {
        public static IEnumerable<T> Content = _content;

        private static readonly List<T> _content = new();

        public static int Count => _content.Count;

        public static int Add(T type)
        {
            int count = Count;
            _content.Add(type);
            return count;
        }

        public static T Get(int index) => index < 0 || index >= _content.Count ? null : _content[index];

        public static bool TryGet(int index, out T instance)
        {
            if (index < 0 || index >= _content.Count)
            {
                instance = null;
                return false;
            }
            instance = _content[index];
            return true;
        }

        public static void Load()
        {

        }

        public static void Unload()
        {
            _content.Clear();
        }
    }

    public sealed class AbilityLoader : QuasarLoader<Ability> { }

    public sealed class ElementLoader : QuasarLoader<Element> { }

    public sealed class FactionLoader : QuasarLoader<Ability> { }

    public sealed class QuasarRarityLoader : QuasarLoader<QuasarRarity> { }

    public sealed class SpecializationLoader : QuasarLoader<Specialization> { }

    public sealed class StatusEffectLoader : QuasarLoader<StatusEffect> { }

    public sealed class TraitLoader : QuasarLoader<Trait> { }
}