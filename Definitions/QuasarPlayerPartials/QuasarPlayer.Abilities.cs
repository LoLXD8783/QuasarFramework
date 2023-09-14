using QuasarFramework.Definitions.AbilityTypes;
using QuasarFramework.Systems;

namespace QuasarFramework.Definitions.QuasarPlayerPartials
{
    public partial class QuasarPlayer : ModPlayer
    {
        public Ability passiveAbility;

        public int energyCurrent;

        public int energyMaximum;

        private int _oneSecond = 60;

        public List<Ability> playerAbilities = new(4);

        public static void CastAbility(Ability ability, ModKeybind castKeybind)
        {
            if (ability is Ability_SingleCast && castKeybind.JustPressed)
                ability.CastMe();

            if (ability is Ability_ChargeCast && castKeybind.Current)
                ability.CastMe();

            if (ability is Ability_ToggleCast && castKeybind.JustPressed)
            {
                ability.CastMe();
                ability.isActive ^= true;
            }
        }

        public void EnergyRegeneration()
        {
            _oneSecond--;

            if (_oneSecond <= 0)
            {
                energyCurrent += 1;

                //combat text

                _oneSecond = 60;
            }
        }

        public void UpdateAbilitySets()
        {
            passiveAbility = playerSpecialization.passiveAbility;

            playerSpecialization.abilities[0].abilityKeybind = KeybindSystem.Ability_FirstKeybind;
            playerSpecialization.abilities[1].abilityKeybind = KeybindSystem.Ability_SecondKeybind;
            playerSpecialization.abilities[2].abilityKeybind = KeybindSystem.Ability_ThirdKeybind;
            playerSpecialization.abilities[3].abilityKeybind = KeybindSystem.Ability_UltimateKeybind;

            playerAbilities[0] = playerSpecialization.abilities[0];
            playerAbilities[1] = playerSpecialization.abilities[1];
            playerAbilities[2] = playerSpecialization.abilities[2];
            playerAbilities[3] = playerSpecialization.abilities[3];
        }

        public void UpdateAbilities()
        {
            passiveAbility?.PassiveEffect(this);

            foreach (Ability ab in playerAbilities)
            {
                if (ab.castCooldownCurrent > 0)
                    ab.castCooldownCurrent--;
            }
        }

        public virtual void ModifyRegenerationRate() { }
    }
}