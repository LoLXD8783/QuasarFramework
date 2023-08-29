namespace QuasarFramework.Definitions.QuasarPlayerPartials
{
    public partial class QuasarPlayer : ModPlayer
    {
        public int experienceCurrent;

        public int experienceLevelRequirement;

        private const int _expLevelReqPost2 = 300000;

        private const int _expLevelReqPost = 450000;
            
        public int experienceTotal;

        public int levelCurrent;

        public int levelMaximum = 40;

        private static int RecalculateExperienceCap(int currentLevel) => (int)Math.Round((-(1 - Math.Pow(1.55, (currentLevel - 1))) * 100));

        public void LevelUp() 
        {
            //play sound

            //play animation

            experienceTotal += experienceCurrent;

            experienceCurrent = 0;

            levelCurrent += 1;

            if (levelCurrent >= levelMaximum)
            {
                levelCurrent = levelMaximum;

                experienceLevelRequirement = _expLevelReqPost2;

                PostLevelCapReward();
            }
                

            if (levelCurrent >= 20)
                experienceLevelRequirement = _expLevelReqPost;

            else
                experienceLevelRequirement = RecalculateExperienceCap(levelCurrent);

            OnLevelUp();

            //display stats up window

            //choose reward window
        }

        public void PostLevelCapReward()
        {

        }

        public virtual void OnLevelUp() { }
    }
}