namespace QuasarFramework.Systems
{
    internal class KeybindSystem : ModSystem
    {
        public static ModKeybind Ability_FirstKeybind { get; internal set; }

        public static ModKeybind Ability_SecondKeybind { get; internal set; }

        public static ModKeybind Ability_ThirdKeybind { get; internal set; }

        public static ModKeybind Ability_UltimateKeybind { get; internal set; }

        public static ModKeybind ChatTabKeybind { get; internal set; }

        public static ModKeybind ReloadKeybind { get; internal set; }

        public static ModKeybind SprintKeybind { get; internal set; }

        public override void Load()
        {
            Ability_FirstKeybind = KeybindLoader.RegisterKeybind(Mod, "Cast First Ability", Keys.Q);

            Ability_SecondKeybind = KeybindLoader.RegisterKeybind(Mod, "Cast Second Ability", Keys.C);

            Ability_ThirdKeybind = KeybindLoader.RegisterKeybind(Mod, "Cast Third Ability", Keys.V);

            Ability_UltimateKeybind = KeybindLoader.RegisterKeybind(Mod, "Cast Ultimate", Keys.F);

            ChatTabKeybind = KeybindLoader.RegisterKeybind(Mod, "Open/Close Chat Tab", Keys.H);

            ReloadKeybind = KeybindLoader.RegisterKeybind(Mod, "Reload Ranged Weapon", Keys.R);

            SprintKeybind = KeybindLoader.RegisterKeybind(Mod, "Sprint", Keys.LeftShift);

            base.Load();
        }

        public override void Unload()
        {
            Ability_FirstKeybind = null;

            Ability_SecondKeybind = null;

            Ability_ThirdKeybind = null;

            Ability_UltimateKeybind = null;

            ChatTabKeybind = null;

            ReloadKeybind = null;

            SprintKeybind = null;

            base.Unload();
        }
    }
}