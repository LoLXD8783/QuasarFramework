namespace QuasarFramework.Definitions.AbilityTypes
{
    public abstract class Ability_ChargeCast : Ability
    {
        internal int chargeLevelCurrent;

        public int[] chargeThreshholds;

        public virtual void OnCast(int chargeLevel) { }

        public virtual void UpdateWhileCharge() { }

        internal sealed override void OnCastType()
        {
            int storedLevel = 0;

            chargeLevelCurrent++;

            UpdateWhileCharge();

            if (abilityKeybind.JustReleased)
            {
                for (int i = 0; i < chargeThreshholds.Length; i++)
                {
                    if (chargeLevelCurrent <= chargeThreshholds[i] && i == 0) //if we are on the first iteration and the charge level is less than the lowest charge
                        break; //cast at weakest level (0)

                    if (chargeLevelCurrent >= chargeThreshholds.Max()) //if our charge level is greater than or equal to the maximum value...
                    {
                        storedLevel = chargeThreshholds.Length; //cast at maximum level
                        break; //exit the loop
                    }

                    if (chargeLevelCurrent >= chargeThreshholds[i]) //if we are greater than the next charge threshhold 
                    {
                        storedLevel = i; //cast at current iteration
                        continue; //continue
                    }
                }

                OnCast(storedLevel);
            }

            base.OnCastType();
        }
    }
}