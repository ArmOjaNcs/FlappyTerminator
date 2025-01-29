using System;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public event Action<bool> RotateToMax;
    public event Action<bool> RotateToMin;
    public event Action<bool> FlyUp;
    public event Action<bool> Shoot;
    public event Action Reload;
    public event Action Paused;
    public event Action UnPaused;

    private bool IsRotateToMax => Input.GetAxis("Mouse Y") > 0;
    private bool IsRotateToMin => Input.GetAxis("Mouse Y") < 0;
    private bool IsFlyUp => Input.GetKeyDown(KeyCode.Space);
    private bool IsShoot => Input.GetKey(KeyCode.Mouse0);
    private bool IsReload => Input.GetKeyDown(KeyCode.Mouse1);
    private bool IsPaused => Input.GetKeyDown(KeyCode.Escape);
    private bool IsUnPaused => Input.GetKeyDown(KeyCode.E);

    private void Start()
    {
        GameUtils.LockCursor();
    }

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

        if (IsReload)
            Reload?.Invoke();

        if(IsPaused)
            Paused?.Invoke();

        if(IsUnPaused)
            UnPaused?.Invoke();
    }
}