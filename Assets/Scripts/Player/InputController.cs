using System;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public event Action<bool> RotateToMax;
    public event Action<bool> RotateToMin;
    public event Action<bool> FlyUp;
    public event Action<bool> Shoot;

    private bool IsRotateToMax => Input.GetKey(KeyCode.W);
    private bool IsRotateToMin => Input.GetKey(KeyCode.S);
    private bool IsFlyUp => Input.GetKeyDown(KeyCode.Space);
    private bool IsShoot => Input.GetKey(KeyCode.E);

    private void Update()
    {
        if (IsRotateToMax)
            RotateToMax?.Invoke(true);
        else
            RotateToMax?.Invoke(false);

        if (IsRotateToMin)
            RotateToMin?.Invoke(true);
        else
            RotateToMin?.Invoke(false);

        if(IsFlyUp)
            FlyUp?.Invoke(true);
        else 
            FlyUp?.Invoke(false);

        if(IsShoot)
            Shoot?.Invoke(true);
        else 
            Shoot?.Invoke(false);
    }
}