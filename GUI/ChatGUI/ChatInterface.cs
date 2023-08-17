namespace QuasarFramework.GUI.ChatGUI 
{ 
    internal class ChatInterface : UIState
    {
        public List<ChatTab> chatTabs;

        //internal panel

        public ChatTab GlobalChatTab;

        public ChatTab LFGChatTab;

        public ChatTab TradingChatTab;

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