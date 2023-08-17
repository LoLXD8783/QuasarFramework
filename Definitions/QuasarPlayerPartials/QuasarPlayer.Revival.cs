namespace QuasarFramework.Definitions.QuasarPlayerPartials
{
    public partial class QuasarPlayer : ModPlayer
    {
        private int _respawnAnchorSetTimer = 60;

        public bool canRevive;

        public int reviveTimer;

        public const int selfReviveTimer = 300;

        public const int selfReviveTimerHard = 9000;

        public Vector2 respawnAnchorLastPos;

        public Vector2 GetRespawnAnchorPosition()
        {
            if (Player.whoAmI != Main.myPlayer)
                return Vector2.Zero;

            else
            {
                Vector2 currentPos = new(Player.position.X, Player.position.Y);
                return currentPos;
            }
        }

        public void ReviveMe()
        {

            OnRevive();
        }

        /// <summary> Allows you to make things happen when this player is revived. <para></para> Useful for resetting effects or states. </summary>
        public virtual void OnRevive() { }

        public override void PreUpdateMovement()
        {
            if (Player.velocity.Y <= 0)
                _respawnAnchorSetTimer--;

            if (_respawnAnchorSetTimer <= 0)
            {
                Vector2 storedPos = respawnAnchorLastPos;

                respawnAnchorLastPos = GetRespawnAnchorPosition();

            }

            base.PreUpdateMovement();
        }
    }
}
