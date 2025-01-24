using UnityEngine;

public class Score : MonoBehaviour
{
    public int Value { get; private set; }

    public void AddValue(int value)
    {
        Value += value;
    }
}