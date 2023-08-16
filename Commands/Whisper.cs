namespace QuasarFramework.Commands
{
    internal class Whisper : ModCommand
    {
        public override CommandType Type => CommandType.Chat;

        public override string Command => "whisper";

        public override string Usage => 
            "/whisper [playername] [message]" +
            "\n playername - The name of the player you're trying to whisper to." +
            "\n message - The message you're whispering to the aforementioned player.";

        public override string Description => "'Whispers' a message to a player on the same server.";

        public override void Action(CommandCaller caller, string input, string[] args)
        {

            if (!Main.dedServ)
                return;

            if (args.Length == 0)
                throw new UsageException("At least one argument is expected");
        }
    }
}