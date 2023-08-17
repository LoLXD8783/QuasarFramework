namespace QuasarFramework.Definitions.QuasarPlayerPartials
{
    public partial class QuasarPlayer : ModPlayer
    {
        public Ability passiveAbility;

        public int energyCurrent;

        public int energyMaximum;

        public List<Ability> playerAbilities = new(4);

        public void CastAbility(Ability ability)
        {

        }

        public void EnergyRegeneration()
        { 

        }

        public void UpdateAbilitySets()
        {
            passiveAbility = playerSpecialization.passiveAbility;

            playerAbilities[0] = playerSpecialization.abilities[0];
            playerAbilities[1] = playerSpecialization.abilities[1];
            playerAbilities[2] = playerSpecialization.abilities[2];
            playerAbilities[3] = playerSpecialization.abilities[3];
        }

        public void UpdateAbilities()
        {
            foreach (Ability ab in playerAbilities)
            {
                if (ab.castCooldown > 0)
                    ab.castCooldown--;

                
            }
        }

        public virtual void ModifyRegenerationRate() { }
    }
}