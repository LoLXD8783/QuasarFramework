namespace QuasarFramework.GUI.Elements
{
    public class SafeUIElement : UIElement
    {
        #region (LEFT) CLICK
        public virtual void SafeClick(UIMouseEvent evt) { }
        public sealed override void LeftClick(UIMouseEvent evt)
        {
            base.LeftClick(evt);
            SafeClick(evt);
        }
        public virtual void SafeClickDouble(UIMouseEvent evt) { }
        public sealed override void LeftDoubleClick(UIMouseEvent evt)
        {
            base.LeftDoubleClick(evt);
            SafeClickDouble(evt);
        }
        public virtual void SafeDown(UIMouseEvent evt) { }
        public sealed override void LeftMouseDown(UIMouseEvent evt)
        {
            base.LeftMouseDown(evt);
            SafeDown(evt);
        }
        public virtual void SafeUp(UIMouseEvent evt) { }
        public sealed override void LeftMouseUp(UIMouseEvent evt)
        {
            base.LeftMouseUp(evt);
            SafeUp(evt);
        }
        #endregion

        public virtual void SafeMouseOver(UIMouseEvent evt) { }
        public sealed override void MouseOver(UIMouseEvent evt)
        {
            base.MouseOver(evt);
            SafeMouseOver(evt);
        }

        public virtual void SafeUpdate(GameTime gameTime) { }
        public sealed override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            SafeUpdate(gameTime);
        }

        public virtual void SafeScroll(UIScrollWheelEvent evt) { }
        public sealed override void ScrollWheel(UIScrollWheelEvent evt)
        {
            base.ScrollWheel(evt);
            SafeScroll(evt);
        }
    }
}