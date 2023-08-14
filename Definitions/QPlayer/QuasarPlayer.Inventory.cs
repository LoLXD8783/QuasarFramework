namespace QuasarFramework.Definitions.QPlayer
{
    public partial class QuasarPlayer : ModPlayer
    {
        public Dictionary<Material, int> materialInventory;

        /// <summary>
        /// Adds a given <see cref="Material"/> to the player's <see cref="materialInventory"/> Dictionary.
        /// </summary>
        /// <param name="player">The player this material is being given to.</param>
        /// <param name="material">The material being given to this player.</param>
        /// <param name="amount">The amount of the material being given.</param> //I ask that you don't judge this even though item.stack exists THANK YOU
        public void AddToMatInventory(QuasarPlayer player, Material material, int amount)
        {
            if (player.Player.whoAmI != Main.myPlayer)
                return;

            if (materialInventory is null)
                return;

            if (materialInventory.ContainsKey(material))
                materialInventory[material] += amount;

            else
            {
                materialInventory.Add(material, amount);
                Main.NewText($"Added {amount} {material.Name} to {player.Name}'s material inventory.");
            }
        }
    }
}
