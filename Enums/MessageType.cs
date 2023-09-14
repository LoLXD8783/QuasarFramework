namespace QuasarFramework.Enums
{

    public enum PlayerMessageType : byte
    {
        ExperienceSync,
        MaterialInventorySync
    }

    public enum QuestMessageType : byte
    {
        Inactive,
        Active,
        InProgress,
        SyncStep,
        SyncProgress,
        Complete,
    }

    public enum WorldMessageType : byte
    {
        WorldBossSpawn,
    }
}