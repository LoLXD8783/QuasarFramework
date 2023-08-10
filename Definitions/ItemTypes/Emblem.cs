namespace QuasarFramework.Definitions.ItemTypes
{
    public abstract class Emblem : QuasarItem
    {
        //emblems cosmetically change the way the inventory / menus look when equipped.
        //Involves manual replacement of textures and the returning of said textures.

        public Texture2D emblemBanner;

        public Texture2D emblemIcon;
    }
}