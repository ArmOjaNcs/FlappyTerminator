using UnityEngine;

public class Score : MonoBehaviour
{
    public int Value {  get; private set; }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Value++;
            Debug.Log(Value);
        }
    }
}