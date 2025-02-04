using System;
using UnityEngine;

public class Score : MonoBehaviour
{
    public event Action<int> ScoreValueUpdate;
    public event Action<int> EnemiesKilledUpdate;

    public int Value { get; private set; }
    public int EnemiesKilled { get; private set; }

    public void AddValue(int value)
    {
        if (value < 0)
            return;

        Value += value;
        ScoreValueUpdate?.Invoke(Value);
    }

    public void AddEnemiesKilled()
    {
        EnemiesKilled++;
        EnemiesKilledUpdate?.Invoke(EnemiesKilled);
    }
}