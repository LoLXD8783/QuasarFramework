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
                KillMe();

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

    //Respawn Handler
    partial class QuasarPlayer : ModPlayer
    {
        private int _respawnAnchorSetTimer = 60;

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

    //Health / Resource Handler

    partial class QuasarPlayer : ModPlayer
    {
        /// <summary>
        /// Maximum => The maximum amount of health a player can have. <para></para>
        /// Current => The Current amount of health a player has in a given instance. <para></para>
        /// "Over" => The amount of additive-decaying health given from life-steal and other temporary effects.
        /// </summary>
        public int healthMaximum, healthCurrent, overHealth;

        public int shieldsMaximum, shieldsCurrent;

        

        public void KillMe()
        {

            if (Player.whoAmI != Main.myPlayer)
                return;

            else
            {
                Player.active = false;
                Player.dead = true;

                //print death reason

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

        public virtual void OnKill() { }

        private double ManualHurtDetour(On_Player.orig_Hurt_PlayerDeathReason_int_int_bool_bool_bool_int_bool_float orig, Player self, PlayerDeathReason damageSource, int Damage, int hitDirection, bool pvp, bool quiet, bool Crit, int cooldownCounter, bool dodgeable, float armorPenetration)
        {
            if (overHealth > 0 )
            {
                if (overHealth > Damage)
                    overHealth -= Damage;

                else
                {
                    Damage -= overHealth;
                    overHealth = 0;
                }
            }

            if (shieldsCurrent > 0)
            {
                if (shieldsCurrent > Damage)
                    shieldsCurrent -= Damage;

                else
                {
                    Damage -= shieldsCurrent;
                    shieldsCurrent = 0;
                }
            }

            if (healthCurrent > 0)
            {
                //armor calculations here

                healthCurrent -= Damage;
            }

            return orig.Invoke(self, damageSource, 0, 0, false, true, false, 0, false, 0.0f);
        }
    }
}