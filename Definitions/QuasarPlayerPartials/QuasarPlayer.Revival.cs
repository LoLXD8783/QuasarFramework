namespace QuasarFramework.Definitions.QuasarPlayerPartials
{
    public partial class QuasarPlayer : ModPlayer
    {
        private int _respawnAnchorSetTimer = 60;

        public bool canRevive;

        public int reviveTimer;

        public const int selfReviveTimer = 300; //self res timer for all patrol / local / easy activities.

        public const int selfReviveTimerHard = 9000; //self res timer for all hard / 3-man activities.

        public Vector2 respawnAnchorLastPos;

        public Vector2 TryGetRespawnAnchorPosition()
        {
            if (Player.whoAmI != Main.myPlayer)
                return Vector2.Zero;

            else
            {
                Vector2 currentPos = new(Player.position.ToWorldCoordinates().X, Player.position.ToWorldCoordinates().Y);
                return currentPos;
            }
        }

        public void ReviveMe(QuasarPlayer player)
        {
            Player.SpawnX = (int)respawnAnchorLastPos.ToWorldCoordinates().X;

            Player.SpawnY = (int)respawnAnchorLastPos.ToWorldCoordinates().Y;

            Player.Spawn(PlayerSpawnContext.ReviveFromDeath);

            OnRevive(player);
        }

        /// <summary> Allows you to make things happen when this player is revived. <para></para> Useful for resetting effects or states. </summary>
        public virtual void OnRevive(QuasarPlayer player) { }

        public override void PreUpdateMovement()
        {
            if (Player.velocity.Y <= 0)
                _respawnAnchorSetTimer--;

            if (_respawnAnchorSetTimer <= 0)
            {
                Vector2 storedPos = respawnAnchorLastPos;

                respawnAnchorLastPos = TryGetRespawnAnchorPosition();


            }

            base.PreUpdateMovement();
        }
    }
}