namespace QuasarFramework.Definitions.QPlayer
{
    public partial class QuasarPlayer : ModPlayer
    {
        /// <summary> Whether or not this player will use pre-determined hit effects. <para></para> Return false to write your own logic in <see cref="OnHit()"/> </summary>
        public bool useVanillaHitEffects = true;

        /// <summary>
        /// Maximum => The maximum amount of health a player can have. <para></para>
        /// Current => The Current amount of health a player has in a given instance. <para></para>
        /// "Over" => The amount of additive-decaying health given from life-steal and other temporary effects.
        /// </summary>
        public int healthMaximum, healthCurrent, overHealth;

        public int shieldsMaximum, shieldsCurrent;

        /// <summary> Automatically kills the player. </summary>
        public void KillMe()
        {

            if (Player.whoAmI != Main.myPlayer)
                return;

            else
            {
                Player.active = false;
                Player.dead = true;

                //print death reason
                // ||
                // \/
                //if we are in MP: print one for me, and one for the other players
                //if we are in solo: print one for me.

                //spawn respawn anchor

                Main.NewText($"{Player.name}", Color.Red);
                OnKill();
            }
        }

        public void OverHealthDecay()
        {
            if (overHealth <= 0)
                return;

            else
                overHealth--;
        }

        private void PostHitEffects()
        {

        }

        /// <summary> Calculates the percentage damage reduction from the player's <see cref="armorTotal"/>. </summary>
        /// <param name="totalArmor">The amount of armor the player has.</param>
        /// <returns></returns>
        private static float ArmorDamageReduction(int totalArmor)
        {
            float damageReduction = (float)(1 - (1 / ((0.17 * (totalArmor / 100)) + 1)));

            //for every 100 armor, our resistance goes up by a given percentage. this is not linear, as it would easily outscale into an inconceivable percentage.
            //thus, a hyperbolic model was chosen to better simulate an infinite scalar that will never reach 100%.

            //for information of how this graph looks, visit https://www.desmos.com/calculator/es0rugng2v

            //(it's really simple but the graph helps visualize!!!)

            return damageReduction;
        }

        #region VIRTUALS

        /// <summary> Allows you to make things happen when the player dies. </summary>
        public virtual void OnKill() { }

        /// <summary> Allows you to make things happen after the player has already been hit. </summary>
        public virtual void OnHit() { }

        /// <summary> Allows you to make things happen after Overhealth is hit. <para></para> This method is only called if the hit is absorbed entirely by Overhealth. </summary>
        public virtual void OnHitOverhealth() { }

        /// <summary> Allows you to make things happen after Shields are hit. <para></para> This method is only called if the hit is absorbed entirely by Shields. </summary>
        public virtual void OnHitShields() { }

        /// <summary> Allows you to make things happen prior to the player being hit. </summary>
        public virtual void PreHit() { }

        #endregion

        /// <summary> Detour of <see cref="Terraria.Player.Hurt(Player.HurtInfo, bool)"/> to redirect damage to our own health pool.</summary>
        private double ManualHurtDetour(On_Player.orig_Hurt_PlayerDeathReason_int_int_bool_bool_bool_int_bool_float orig, Player self, PlayerDeathReason damageSource, int Damage, int hitDirection, bool pvp, bool quiet, bool Crit, int cooldownCounter, bool dodgeable, float armorPenetration)
        {

            PreHit();

            if (overHealth > 0) //overhealth is first. hopefully you have any before getting hit! (adjust decay rate maybe?)
            {
                if (overHealth > Damage)
                    overHealth -= Damage; //overhealth is literally shields but they also decay

                else
                {
                    Damage -= overHealth; //despite being called health, they don't take armor reduction

                    OnHitOverhealth();

                    overHealth = 0;
                }
            }

            if (shieldsCurrent > 0) //second is shields. these regen much faster than health (if your specialist has them or you build towards them)
            {
                if (shieldsCurrent > Damage)
                    shieldsCurrent -= Damage;

                else
                {
                    Damage -= shieldsCurrent;

                    OnHitShields();

                    shieldsCurrent = 0;
                }
            }

            if (healthCurrent > 0) //health is the fun one.
            {
                float damageReductionFromArmor = ArmorDamageReduction(armorTotal);

                Damage = (int)Math.Round(Damage * damageReductionFromArmor); //we ensure it stays an int (I wanted to use doubles but it's more annoying that way)

                healthCurrent -= Damage; //since having damage equal to or greater than our health will kill us, we simply don't care about reduction after the fact.
            }

            if (!useVanillaHitEffects)
                OnHit();

            else
            {
                PostHitEffects();
                OnHit();
            }

            return orig.Invoke(self, damageSource, 0, 0, false, true, false, 0, false, 0.0f); //might remove this invocation.
        }
    }
}