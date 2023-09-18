namespace QuasarFramework.Definitions
{
    public struct SkillTreeNodeData
    {
        private string _name;

        public bool isActive;

        public bool isUnlocked;

        public int nodeLevel; //current node level

        private int nodeLevelMax; //user-set maximum for specific levels.

        public List<QuasarItem> itemRequirements;

        public SkillTreeNodeData()
        {
            isActive = false;
            isUnlocked = false;
            nodeLevel = 0;
            nodeLevelMax = 1;
        }

        public SkillTreeNodeData(string name, int nodeLevelMax)
        {
            _name = name;
            this.nodeLevelMax = nodeLevelMax;

            isActive = false;
            isUnlocked = false;
            nodeLevel = 0;
        }

        public void UnlockMe()
        {

        }

        public void UpgradeMe()
        {

        }
    }
}