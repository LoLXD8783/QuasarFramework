namespace QuasarFramework.Definitions.ItemTypes
{
    public abstract class Material : QuasarItem
    {
        public override bool OnPickup(Player player)
        {
            QuasarPlayer playerInst = player.GetModPlayer<QuasarPlayer>(); //we get the instance of quasarplayer

            playerInst.AddToMatInventory(playerInst, this, Item.stack); //add it to the internal inventory

            Item.TurnToAir(); //we turn to air to make it "disappear" and give the illusion of it having gone into the inventory.

            return false; //we don't want to actually add it to the inventory so, to be safe, we return false (even if it is air).
        }

        public override void SetDefaults()
        {
            Item.material = true;

            Item.maxStack = int.MaxValue; //we want an """infinite""" stack since materials can drop in large quantities. (500-2500 and so on if stacked)

            base.SetDefaults();
        }
    }
}