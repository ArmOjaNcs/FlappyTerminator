using System;
using UnityEngine;

public class PlayerInput : MonoBehaviour, IPauseable
{
    [SerializeField] private Pause _pause;

    private bool _isPaused;

    public event Action<bool> RotateToMax;
    public event Action<bool> RotateToMin;
    public event Action<bool> FlyUp;
    public event Action<bool> FlyDown;
    public event Action StartShoot;
    public event Action StopShoot;
    public event Action Reload;
    public event Action Paused;
    public event Action UnPaused;
    public event Action Upgrade;

    private bool IsRotateToMax => Input.GetAxis(GameUtils.MouseY) > 0;
    private bool IsRotateToMin => Input.GetAxis(GameUtils.MouseY) < 0;
    private bool IsFlyUp => Input.GetKey(KeyCode.UpArrow);
    private bool IsFlyDown => Input.GetKey(KeyCode.DownArrow);
    private bool IsShoot => Input.GetKey(KeyCode.Mouse0);
    private bool IsReload => Input.GetKeyDown(KeyCode.Mouse1);
    private bool IsPaused => Input.GetKeyDown(KeyCode.Escape);
    private bool IsUnPaused => Input.GetKeyDown(KeyCode.RightArrow);
    private bool IsUpgrade => Input.GetKeyDown(KeyCode.LeftArrow);

    private void Awake()
    {
        _pause.Register(this);
    }

    private void Start()
    {
        GameUtils.LockCursor();
    }

    private void Update()
    {
        if (_isPaused == false)
        {
            if (IsRotateToMax)
                RotateToMax?.Invoke(true);
            else
                RotateToMax?.Invoke(false);

            if (IsRotateToMin)
                RotateToMin?.Invoke(true);
            else
                RotateToMin?.Invoke(false);

            if (IsFlyUp)
                FlyUp?.Invoke(true);
            else
                FlyUp?.Invoke(false);

            if (IsShoot)
                StartShoot?.Invoke();
            else
                StopShoot?.Invoke();

            if (IsFlyDown)
                FlyDown?.Invoke(true);
            else
                FlyDown?.Invoke(false);

            if (IsReload)
                Reload?.Invoke();
        }

        if (IsPaused)
            Paused?.Invoke();

        if (IsUnPaused)
        {
            UnPaused?.Invoke();
            Debug.Log("Unpaused");
        }

        if(IsUpgrade)
            Upgrade?.Invoke();
    }

    public void Stop()
    {
        _isPaused = true;
        GameUtils.UnlockCursor();
    }

    public void Resume()
    {
        _isPaused = false;
        GameUtils.LockCursor();
    }
}