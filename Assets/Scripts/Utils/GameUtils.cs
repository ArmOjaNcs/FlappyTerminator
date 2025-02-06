using UnityEngine;

public static class GameUtils
{
    public static readonly int StartEnemiesCount = 3;
    public static readonly int StartObstaclesCount = 1;
    public static readonly int MaxBoostLevel = 18;
    public static readonly int ScoreByEnemy = 5;
    public static readonly int DividerForAddMaxEnemyCount = 6;
    public static readonly int DividerForAddMaxObstaclesCount = 12;
    public static readonly int DividerForDecreasePlayerBullet = 2;
    public static readonly int DiminutiveForMedPack = 2;
    public static readonly int EnemyBulletMultiplier = 2;
    public static readonly float StartEnvironmentSpeed = 8;
    public static readonly float EnvironmentBoostedSpeed = 2.0f;
    public static readonly float TimeToAddScore = 1;
    public static readonly float TimeToNextLevel = 5;
    public static readonly float TimeToSpawnMedPack = 65;
    public static readonly float MinXPosition = 40;
    public static readonly float MaxXPosition = 350;
    public static readonly float MinXPositionForCratesSpawner = 200;
    public static readonly float MaxXPositionForCratesSpawner = 240;
    public static readonly float AnimationBoost = 0.1f;
    public static readonly string MainScene = nameof(MainScene);
    public static readonly string MenuScene = nameof(MenuScene);
    public static readonly string MouseY = "Mouse Y";

    public static float CurrentGroundSpeed = StartEnvironmentSpeed;

    public static void UnlockCursor()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public static void LockCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public static void AddCurrentGroundSpeed()
    {
        CurrentGroundSpeed += EnvironmentBoostedSpeed;
    }
}