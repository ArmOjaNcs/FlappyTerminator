using UnityEngine;

public class PlayerRotator : MonoBehaviour, IPauseable
{
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _maxRotationZ;
    [SerializeField] private float _minRotationZ;

    private Quaternion _maxRotation;
    private Quaternion _minRotation;
    private bool _isRotateToMax;
    private bool _isRotateToMin;
    private bool _isPaused;

    private void Awake()
    {
        _maxRotation = Quaternion.Euler(0, 0, _maxRotationZ);
        _minRotation = Quaternion.Euler(0, 0, _minRotationZ);
    }

    private void Update()
    {
        if (_isPaused == false)
            Rotate();
    }

    public void Stop()
    {
        _isPaused = true;
    }

    public void Resume()
    {
        _isPaused = false;
    }

    public void RotateToMax(bool isRotate)
    {
        _isRotateToMax = isRotate;
    }

    public void RotateToMin(bool isRotate)
    {
        _isRotateToMin = isRotate;
    }

    private void Rotate()
    {
        if (_isRotateToMax)
            LerpRotation(_maxRotation);
        else if (_isRotateToMin)
            LerpRotation(_minRotation);
    }

    private void LerpRotation(Quaternion rotation)
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, _rotationSpeed * Time.deltaTime);
    }
}