namespace QuasarFramework.Definitions
{
    public class QuasarPlayer : ModPlayer
    {
        public Dictionary<Element, int> elementResistance;

        /// <summary>
        /// Replaces Defense as the damage reduction for armor and other modifiers.
        /// </summary>
        public int armorTotal;

        public int energyMaximum, energyCurrent;

        /// <summary>
        /// Current => The current amount of experience the player has at a given instance per level. <para></para>
        /// Total => The total amount of experience earned from all levels.
        /// </summary>
        public int experienceCurrent, experienceTotal;

        public int levelMaximum, levelCurrent;

        /// <summary>
        /// Maximum => The maximum amount of health a player can have. <para></para>
        /// Current => The Current amount of health a player has in a given instance. <para></para>
        /// "Over" => The amount of additive-decaying health given from life-steal and other temporary effects.
        /// </summary>
        public int healthMaximum, healthCurrent, overHealth;

        public int shieldsMaximum, shieldsCurrent;

        /// <summary>
        /// The player's Specialization, or "class"
        /// </summary>
        public Specialization playerSpecialization;

        public void OverHealthDecay()
        {
            if (overHealth <= 0)
                return;

            else
                overHealth -= 1;
        }

        public void KillMe()
        {
            
        }

        public virtual void OnKill() { }

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
            elementResistance ??= new();

            base.Load();
        }

        #region UPDATERS

        /// <summary>
        /// Updates logic pertaining to a player's specialization.
        /// </summary>
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

            base.PreUpdate();
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
}