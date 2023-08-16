namespace QuasarFramework.GUI.States
{
    //credit to ScalarVector and co.

    public abstract class SafeUIState : UIState
    {
        protected internal virtual UserInterface UserInterface { get; set; }

        public abstract int InsertionIndex(List<GameInterfaceLayer> layers);

        public virtual bool Visible { get; set; } = false;

        public virtual InterfaceScaleType ScaleType { get; set; } = InterfaceScaleType.UI;

        public virtual void Unload() { }
    }
}