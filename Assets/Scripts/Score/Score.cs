using System;
using UnityEngine;

public class Score : MonoBehaviour
{
    public event Action<int> ScoreUpdate;

    public int Value { get; private set; }

    public void AddValue(int value)
    {
        Value += value;
        ScoreUpdate?.Invoke(Value);
    }
}