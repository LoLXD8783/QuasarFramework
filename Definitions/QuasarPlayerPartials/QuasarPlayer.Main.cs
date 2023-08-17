namespace QuasarFramework.Definitions.QuasarPlayerPartials
{
    //Main Updater
    public partial class QuasarPlayer : ModPlayer
    {
        public Dictionary<Element, int> elementResistance;

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
            tag[nameof(experienceTotal)] = experienceTotal;

            base.SaveData(tag);
        }

        public override void LoadData(TagCompound tag)
        {
            experienceCurrent = tag.Get<int>(nameof(experienceCurrent));
            experienceTotal = tag.Get<int>(nameof(experienceTotal));

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