namespace QuasarFramework.Systems
{
    internal class KeybindSystem : ModSystem
    {
        public ModKeybind ChatTabKeybind { get; internal set; }

        public ModKeybind ReloadKeybind { get; internal set; }

        public override void Load()
        {
            ChatTabKeybind = KeybindLoader.RegisterKeybind(Mod, "Open/Close Chat Tab", Keys.H);

            ReloadKeybind = KeybindLoader.RegisterKeybind(Mod, "Reload Ranged Weapon", Keys.R);

            base.Load();
        }

        public override void Unload()
        {
            ChatTabKeybind = null;

            ReloadKeybind = null;

            base.Unload();
        }
    }
}