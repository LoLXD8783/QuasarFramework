namespace QuasarFramework.GUI.ChatGUI 
{ 
    internal class IngameChatInterface : UIState
    {
        public List<IngameChatTab> chatTabs;

        //internal panel

        public IngameChatTab GlobalChatTab;

        public IngameChatTab LFGChatTab;

        public IngameChatTab TradingChatTab;

        //tab globalchattab

        //tab lfgchattab

        //tab tradingchattab

        public override void OnInitialize()
        {
            chatTabs ??= new();

            chatTabs.Add(GlobalChatTab);
            chatTabs.Add(LFGChatTab);
            chatTabs.Add(TradingChatTab);



            base.OnInitialize();
        }

        public override void Update(GameTime gameTime)
        {
            //continuously grab chat data

            //add new chat

            base.Update(gameTime);
        }

        public override void LeftMouseDown(UIMouseEvent evt)
        {
            base.LeftMouseDown(evt);
        }
    }
}