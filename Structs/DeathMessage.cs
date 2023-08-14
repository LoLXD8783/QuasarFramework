namespace QuasarFramework.Structs
{
    public struct DeathMessage
    {
        public Entity causeOfDeath;

        public QuasarPlayer affectedPlayer;

        public string deathMessageTitle;

        public string deathMessageText;

        public DeathMessage()
        {

        }

        public DeathMessage(Entity causeOfDeath, QuasarPlayer affectedPlayer, string title, string text)
        {
            this.causeOfDeath = causeOfDeath;
            this.affectedPlayer = affectedPlayer;
            deathMessageTitle = title;
            deathMessageText = text;
        }
    }
}