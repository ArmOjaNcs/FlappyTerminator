public static class UpgradeUtils 
{
    private readonly static int _enemiesForNextLevel = 5;
    private readonly static int _enemiesCountWithUpgrade = 2;

    public readonly static float PercentForEnemyUpgrade = 0.04f;
    public readonly static float PercentForEnemyHealthUpgrade = 0.1f;
    public readonly static float PercentForPlayerUpgrade = 0.1f;
    public readonly static float PercentForPlayerHealthUpgrade = 0.2f;
    public readonly static float PercentForPlayerDamageUpgrade = 0.15f;
    public readonly static float PercentForPlayerLastLevelUpgrade = 0.05f;
    public readonly static int MaxPlayerLevel = 60;
    public readonly static int MaxAbilityLevel = 10;
    public readonly static int BulletsCountOnUpgrade = 10;

    public static int EnemiesForNextLevel { get; private set; }
    public static int NotAcceptedPlayerLevel { get; private set; }

    public static void AddNotAcceptedPlayerLevel()
    {
        NotAcceptedPlayerLevel++;
    }

    public static void AcceptPlayerLevel()
    {
        if(NotAcceptedPlayerLevel > 0) 
            NotAcceptedPlayerLevel--;
    }

    public static void AddEnemiesForNextLevel()
    {
        EnemiesForNextLevel += _enemiesCountWithUpgrade;
    }

    public static void StartGame()
    {
        EnemiesForNextLevel = _enemiesForNextLevel;
    }
}