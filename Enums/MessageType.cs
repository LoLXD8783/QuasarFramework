namespace QuasarFramework.Enums
{

    public enum PlayerMessageType : byte
    {
        ExperienceSync
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