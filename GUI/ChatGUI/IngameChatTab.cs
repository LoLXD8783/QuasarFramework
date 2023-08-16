using QuasarFramework.GUI.Elements;

namespace QuasarFramework.GUI.ChatGUI
{
    internal class IngameChatTab : SafeUIElement
    {
        private readonly ChatTabInner innerBox = new();

        /// <summary> How many messages are viewable in scroll history before they're deleted. </summary>
        public int chatLimit;

        public override void SafeUpdate(GameTime gameTime)
        {
            if (innerBox.chatMessages.Count >= chatLimit)
                innerBox.chatMessages.Remove(innerBox.chatMessages.Last());

            base.SafeUpdate(gameTime);
        }
    }

    internal class ChatTabInner : SafeUIElement
    {
        public List<string> chatMessages;

        public override void SafeUpdate(GameTime gameTime)
        {
            //if recieve message => load and print message to chatbox

            //if send message => send and print message to chatbox

            base.SafeUpdate(gameTime);
        }
    }
}