namespace QuasarFramework.Definitions.NPCTypes
{
    public abstract class Vendor : QuasarNPC
    {
        //vendors are (mostly) stationary NPCs that hand out quests and deal with item transactions.



        public bool BuyFromMe(out QuasarItem item)
        {
            //check required currency
            //if currency isn't sufficient, return false
            //else, proceed with purchase

            OnPurchaseSomething();
            item = null;
            return false;
        }

        public virtual void OnPurchaseSomething() { }
    }
}