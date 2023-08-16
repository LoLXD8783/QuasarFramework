using QuasarFramework.GUI.States;

namespace QuasarFramework.Loaders
{
    //credit to ScalarVector and co. 

    class UILoader : ModSystem
    {
        public static List<UserInterface> _interfaces = new();

        public static List<SafeUIState> _states = new();

        public override void Load()
        {
            if (!Main.dedServ)
                return;

            _interfaces = new();
            _states = new();

            foreach (Type t in Mod.Code.GetTypes())
            {
                if (!t.IsAbstract && t.IsSubclassOf(typeof(SafeUIState)))
                {
                    var state = (SafeUIState)Activator.CreateInstance(t, null);
                    var uInterface = new UserInterface();

                    uInterface.SetState(state);
                    state.UserInterface = uInterface;

                    _states?.Add(state);
                    _interfaces?.Add(uInterface);
                }
            }

            base.Load();
        }

        public override void Unload()
        {
            _states.ForEach(n => n.Unload());
            _interfaces = null;
            _states = null;

            base.Unload();
        }

        public static void AddLayer(List<GameInterfaceLayer> layers, UIState state, int index, bool visible, InterfaceScaleType scaleType)
        {
            string name = state == null ? "Unknown" : state.ToString();

            layers.Insert(index, new LegacyGameInterfaceLayer("QuasarFramework: " + name, 
                delegate 
                { 
                    if (visible)
                        state.Draw(Main.spriteBatch);
                    
                    return true;
                }, 
                scaleType));
        }

        public static T GetState<T>() where T : SafeUIState { return _states.FirstOrDefault(n => n is T) as T; }

        public static void ReloadState<T>() where T : SafeUIState
        {
            int index = _states.IndexOf(GetState<T>());
            _states[index] = (T)Activator.CreateInstance(typeof(T), null);
            _interfaces[index] = new UserInterface();
            _interfaces[index].SetState(_states[index]);
        }

        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            for (int i = 0; i < _states.Count; i++)
            {
                SafeUIState state = _states[i];
                AddLayer(layers, state, state.InsertionIndex(layers), state.Visible, state.ScaleType);
            }

            base.ModifyInterfaceLayers(layers);
        }

        public override void UpdateUI(GameTime gameTime)
        {
            foreach (UserInterface eachState in _interfaces)
            {
                if (eachState?.CurrentState != null && ((SafeUIState)eachState.CurrentState).Visible)
                    eachState.Update(gameTime);
            }

            base.UpdateUI(gameTime);
        }
    }
}