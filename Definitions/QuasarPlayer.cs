namespace QuasarFramework.Definitions
{
    //Main Updater
    public partial class QuasarPlayer : ModPlayer
    {
        public Ability passiveAbility;

        public Dictionary<Element, int> elementResistance;

        /// <summary>Replaces Defense as the damage reduction for armor and other modifiers.</summary>
        public int armorTotal;

        public int energyMaximum, energyCurrent;

        /// <summary>
        /// Current => The current amount of experience the player has at a given instance per level. <para></para>
        /// Total => The total amount of experience earned from all levels.
        /// </summary>
        public int experienceCurrent, experienceTotal;

        public int levelMaximum, levelCurrent;

        public List<Ability> playerAbilities = new(4);

        /// <summary>The player's Specialization, or "class"</summary>
        public Specialization playerSpecialization;

        public override void ResetEffects()
        {
            armorTotal = 50;
            healthMaximum = 200; //base health, shields and energy
            shieldsMaximum = 100;
            energyMaximum = 100;

            foreach (Element element in elementResistance.Keys)
                elementResistance[element] = 0;

            base.ResetEffects();
        }

        public override void Load()
        {
            materialInventory ??= new();

            elementResistance ??= new();

            On_Player.Hurt_PlayerDeathReason_int_int_bool_bool_bool_int_bool_float += ManualHurtDetour;

            base.Load();
        }

        public override void Unload()
        {
            materialInventory = null;

            elementResistance = null;

            base.Unload();
        }

        #region UPDATERS

        /// <summary>Updates logic pertaining to a player's specialization.</summary>
        public virtual void UpdateSpecializations()
        {
            armorTotal += playerSpecialization.additiveArmor;
            energyMaximum += playerSpecialization.additiveEnergy;
            healthMaximum += playerSpecialization.additiveHealth;
            shieldsMaximum += playerSpecialization.additiveShields;
        }

        public override void PreUpdate()
        {
            UpdateSpecializations();

            passiveAbility.PassiveEffect(this);

            base.PreUpdate();
        }

        public override void PostUpdate()
        {
            OverHealthDecay();

            if (healthCurrent <= 0 && overHealth <= 0 && Player.active && !Player.dead) //we check if active and NOT dead to make sure that KillMe() only runs once.
                KillMe(); //you should LOVE yourself... NOW

            base.PostUpdate();
        }

        #endregion

        #region SAVE / LOAD

        public override void SaveData(TagCompound tag)
        {
            tag[nameof(experienceCurrent)] = experienceCurrent;

            base.SaveData(tag);
        }

        public override void LoadData(TagCompound tag)
        {
            experienceCurrent = tag.Get<int>(nameof(experienceCurrent));

            base.LoadData(tag);
        }

        #endregion

        #region NETCODE SHENANIGANS

        public override void SyncPlayer(int toWho, int fromWho, bool newPlayer)
        {
            ModPacket pack = Mod.GetPacket();
            pack.Write((byte)PlayerMessageType.ExperienceSync);
            pack.Write((byte)Player.whoAmI);
            pack.Write((byte)experienceCurrent);
            pack.Send(toWho, fromWho);

            base.SyncPlayer(toWho, fromWho, newPlayer);
        }

        public void RecieveSync(BinaryReader reader)
        {
            experienceCurrent = reader.ReadByte();
        }

        public override void CopyClientState(ModPlayer targetCopy)
        {
            QuasarPlayer clone = (QuasarPlayer)targetCopy;
            clone.experienceCurrent = experienceCurrent;

            base.CopyClientState(targetCopy);
        }

        public override void SendClientChanges(ModPlayer clientPlayer)
        {
            QuasarPlayer clone = (QuasarPlayer)clientPlayer;

            if (experienceCurrent != clone.experienceCurrent)
                SyncPlayer(toWho: -1, fromWho: Main.myPlayer, newPlayer: false);

            base.SendClientChanges(clientPlayer);
        }

        #endregion
    }

    //Inventory handler
    partial class QuasarPlayer : ModPlayer
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

    //Controls Handler
    partial class QuasarPlayer : ModPlayer
    {
        public override void SetControls()
        {


            base.SetControls();
        }
    }

    //Revive Handler
    partial class QuasarPlayer : ModPlayer
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

    //Health, Shields, Armor, Death, and Hits (it sounds like a lot but it's mostly connected here)
    partial class QuasarPlayer : ModPlayer
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

            if (overHealth > 0 ) //overhealth is first. hopefully you have any before getting hit! (adjust decay rate maybe?)
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