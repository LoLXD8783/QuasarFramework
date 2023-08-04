namespace QuasarFramework
{
	//Loader Logger
	partial class QuasarFramework : Mod
	{
		internal static QuasarFramework Instance;

		/// <summary>
		/// Allows you to write messages to Logs.
		/// </summary>
		/// <param name="mod">The mod writing the log</param>
		/// <param name="inputType">The type of message to be put into the log </param>
		/// <param name="text">The message</param>
		internal static void WriteLogger(Mod mod, InputType inputType, string text)
		{
			switch (inputType)
			{
				case InputType.Info:
					mod.Logger.Info(text); break;

				case InputType.Debug:
					mod.Logger.Debug(text); break;

				case InputType.Warn:
					mod.Logger.Warn(text); break;

				case InputType.Error:
					mod.Logger.Error(text); break;

				case InputType.Fatal:
					mod.Logger.Fatal(text); break;
			}
		}

        internal enum InputType
        {
			Info,
            Debug,
            Warn,
            Error,
            Fatal
        }

        public override void Load()
        {
			AbilityLoader.Load();

			ElementLoader.Load();

			TraitLoader.Load();

            base.Load();
        }

        public override void Unload()
        {
			AbilityLoader.Unload();

			ElementLoader.Unload();

			TraitLoader.Unload();

            base.Unload();
		}

	}


	//Netcode Handler
	partial class QuasarFramework : Mod
	{ 
        public override void HandlePacket(BinaryReader reader, int whoAmI)
        {
			PlayerMessageType msgType = (PlayerMessageType)reader.ReadByte();

			switch(msgType)
			{
				case PlayerMessageType.ExperienceSync:
					byte playerNum = reader.ReadByte();
					QuasarPlayer qPlayer = Main.player[playerNum].GetModPlayer<QuasarPlayer>();
					qPlayer.RecieveSync(reader);

					if (Main.netMode is NetmodeID.Server)
						qPlayer.SyncPlayer(-1, whoAmI, false);

					break;
			}

            base.HandlePacket(reader, whoAmI);
        }
    }
}