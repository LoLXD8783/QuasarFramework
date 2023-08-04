namespace QuasarFramework.GUI.Elements
{
    public class ModificationSlot : SafeUIElement
    {
        public Item Item = new();

        //Modifiers function through these slots only.

        //Would've called them "mods" but you can see how that would go.

        //change to augments, maybe?

        //Add in modification types

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (IsMouseHovering)
                Main.LocalPlayer.mouseInterface = true;
             
            Texture2D slotTexture = null;

            spriteBatch.Draw(slotTexture, GetDimensions().Position(), slotTexture.Frame(), Color.White, 0, Vector2.Zero, 1, 0, 0);

            if (!Item.IsAir)
            {
                Texture2D itemTexture = null;

                spriteBatch.Draw(itemTexture, 
                    new Rectangle((int)GetDimensions().X + 30, 
                    (int)GetDimensions().Y + 30, 
                    (int)MathHelper.Min(itemTexture.Width, 28), 
                    (int)MathHelper.Min(itemTexture.Height, 28)), 
                    itemTexture.Frame(), 
                    Color.White, 0, 
                    itemTexture.Size() / 2, 0, 0);

                if (IsMouseHovering)
                {
                    Main.LocalPlayer.mouseInterface = true;
                    Main.HoverItem = Item.Clone();
                    Main.hoverItemName = "a";
                }
            }

            base.Draw(spriteBatch);
        }

        public override void SafeClick(UIMouseEvent evt)
        {
            Player Player = Main.LocalPlayer;

            if (Main.mouseItem.IsAir && !Item.IsAir)
            {
                Main.mouseItem = Item.Clone();
                Item.TurnToAir();
                //playsound
            }

            if (Item.IsAir && Player.HeldItem.type == Item.type)
                //playsound
                return;

            if (Player.HeldItem.ModItem is Modification && Item.IsAir)
            {
                Item = Player.HeldItem.Clone();
                Player.HeldItem.TurnToAir();
                Main.mouseItem.TurnToAir();
                //playSound
            }

            if (Player.HeldItem.ModItem is Modification && !Item.IsAir)
            {
                Item temp = Item;
                Item = Player.HeldItem;
                Main.mouseItem = temp;
                //playSound
            }

            Main.isMouseLeftConsumedByUI = true;

            base.SafeClick(evt);
        }

        public override void SafeUpdate(GameTime gameTime)
        {
            if (Item.type == ItemID.None || Item.stack <= 0)
                Item.TurnToAir();

            Width.Set(60, 0);
            Height.Set(60, 0);

            base.SafeUpdate(gameTime);
        }
    }
}