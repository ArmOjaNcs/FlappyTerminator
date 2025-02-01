using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMover : MonoBehaviour, IPauseable
{
    [SerializeField] private float _tapForce;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _maxRotationZ;
    [SerializeField] private float _minRotationZ;

    private Vector3 _startPosition;
    private Rigidbody2D _rigidbody2D;
    private Quaternion _maxRotation;
    private Quaternion _minRotation;
    private bool _isRotateToMax;
    private bool _isRotateToMin;
    private bool _isFlyUp;
    private bool _isPaused;

    private void Awake()
    {
        _startPosition = transform.position;
        _rigidbody2D = GetComponent<Rigidbody2D>();

        _maxRotation = Quaternion.Euler(0, 0, _maxRotationZ);
        _minRotation = Quaternion.Euler(0, 0, _minRotationZ);
    }

    private void Update()
    {
        if(_isPaused == false)
            Rotate();
    }

    private void FixedUpdate()
    {
        if (_isPaused == false)
            Fly();
    }

    public void Stop()
    {
        _isPaused = true;

        _rigidbody2D.linearVelocity = Vector3.zero;
        _rigidbody2D.bodyType = RigidbodyType2D.Kinematic;
    }

    public void Resume()
    {
        _isPaused = false;

        _rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
    }

    public void RotateToMax(bool isRotate)
    {
        _isRotateToMax = isRotate;
    }

    public void RotateToMin(bool isRotate)
    {
        _isRotateToMin = isRotate;
    }

    public void FlyUp(bool isFlyUp)
    {
        _isFlyUp = isFlyUp;
    }

    private void Fly()
    {
        if (_isFlyUp)
            _rigidbody2D.linearVelocity = Vector2.up * _tapForce;

        _rigidbody2D.angularVelocity = 0;
    }

    private void Rotate()
    {
        if (_isRotateToMax)
            LerpRotation(_maxRotation);
        else if (_isRotateToMin)
            LerpRotation(_minRotation);

        if (transform.position.x > _startPosition.x || transform.position.x < _startPosition.x)
            transform.position = new Vector2(_startPosition.x, transform.position.y);
    }

    private void LerpRotation(Quaternion rotation)
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, _rotationSpeed * Time.deltaTime);
    }
}