using QuasarFramework.Loaders;
using System.Security;

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
					mod.Logger.Info("[QUASAR] " + text); break;

				case InputType.Debug:
					mod.Logger.Debug("[QUASAR] " + text); break;

				case InputType.Warn:
					mod.Logger.Warn("[QUASAR] " + text); break;

				case InputType.Error:
					mod.Logger.Error("[QUASAR] " + text); break;

				case InputType.Fatal:
					mod.Logger.Fatal("[QUASAR] " + text); break;
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
			ArchetypeLoader.Load();

			AbilityLoader.Load();

			ElementLoader.Load();

			FactionLoader.Load();

			QuasarRarityLoader.Load();

			StatusEffectLoader.Load();

			SpecializationLoader.Load();

			TraitLoader.Load();

            base.Load();
        }

        public override void Unload()
        {
			ArchetypeLoader.Unload();

			AbilityLoader.Unload();

			ElementLoader.Unload();

			FactionLoader.Unload();

			QuasarRarityLoader.Unload();

			StatusEffectLoader.Unload();

			SpecializationLoader.Unload();

			TraitLoader.Unload();

			CloseConnection(connection);

            base.Unload();
		}
	}

	//Netcode Handler
	partial class QuasarFramework
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

	//DB Handler
	partial class QuasarFramework
	{
		public string userName;

		public CSteamID userSteamID;

		public SecureString passWord;

		public SqlConnection connection;

		public void LoginToDatabase(string connectString, string username, SecureString password)
		{
			//communicate with UI for this one

			SqlCredential cred = new(username, password);

			OpenConnection(connectString, cred, out connection);
		}

		public static void CloseConnection(SqlConnection conn) => conn.Close();

		public static void OpenConnection(string connectString, SqlCredential cred, out SqlConnection conn)
		{
			SqlConnection connection = new(connectString, cred);
			connection.Open();

			conn = connection;
		}

        public static void Query(string instruction, SqlConnection conn)
        {
            SqlCommand comm = new(instruction, conn);
            using (SqlDataReader reader = comm.ExecuteReader())
            {
                if (reader.Read())
                {

                }
            }
        }
    }
}