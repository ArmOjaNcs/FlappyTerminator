using UnityEngine;

public static class GameUtils
{
    public static readonly int StartEnemiesCount = 3;
    public static readonly int StartObstaclesCount = 1;
    public static readonly int MaxBoostLevel = 12;
    public static readonly int ScoreByEnemy = 5;
    public static readonly int DividerForAddMaxEnemyCount = 6;
    public static readonly int DividerForAddMaxObstaclesCount = 12;
    public static readonly float StartEnvironmentSpeed = 10;
    public static readonly float EnvironmentBoostedSpeed = 2f;
    public static readonly float TimeToAddScore = 1;
    public static readonly float TimeToNextLevel = 5;
    public static readonly float MinXPosition = 40;
    public static readonly float MaxXPosition = 180;
    public static readonly string MainScene = nameof(MainScene);

    public static void UlockCursor()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public static void LockCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}