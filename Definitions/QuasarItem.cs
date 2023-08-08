namespace QuasarFramework.Definitions
{
    public abstract class QuasarItem : ModItem
    {
        /// <summary>
        /// A newly configurable book of tooltips for tooltip manipulation. <para></para>
        /// See documentation for proper usage.
        /// </summary>
        public TooltipBook? tooltipBook;

        /// <summary>
        /// The current page of your tooltip book being displayed.
        /// </summary>
        public int tooltipBookIndex;

        public Asset<Texture2D> itemTypeIcon;

        public virtual void EditTooltipBook(TooltipBook tooltipBook) { }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            EditTooltipBook(tooltipBook);

            foreach (TooltipLine line in tooltipBook.bookPages[tooltipBookIndex].pageLines) //for each tooltip line in our book AT our page index.
                tooltips.Add(line); //add to the current visible tooltips.

            base.ModifyTooltips(tooltips);
        }

        public override void SetDefaults()
        {

            base.SetDefaults();
        }

        public override void SaveData(TagCompound tag)
        {
            base.SaveData(tag);
        }

        public override void LoadData(TagCompound tag)
        {
            base.LoadData(tag);
        }
    }
}