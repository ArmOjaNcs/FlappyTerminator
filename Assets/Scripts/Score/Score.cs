using System;
using UnityEngine;

public class Score : MonoBehaviour
{
    public event Action<int> ScoreUpdate;

    public int Value { get; private set; }

    public void AddValue(int value)
    {
        if (value < 0)
            return;

        Value += value;
        ScoreUpdate?.Invoke(Value);
    }
}