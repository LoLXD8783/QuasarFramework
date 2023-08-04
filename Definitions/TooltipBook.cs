namespace QuasarFramework.Definitions
{
    public class TooltipBook
    {
        public struct TooltipPage
        {
            public Mod mod;

            public string pageTitle;

            public List<TooltipLine> pageLines;

            public TooltipPage(string pageTitle)
            {
                this.pageTitle = pageTitle;
            }
        }

        public List<TooltipPage> bookPages;
    }
}