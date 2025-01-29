using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField] private InputController _inputController;
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

    private void Awake()
    {
        _startPosition = transform.position;
        _rigidbody2D = GetComponent<Rigidbody2D>();

        _maxRotation = Quaternion.Euler(0, 0, _maxRotationZ);
        _minRotation = Quaternion.Euler(0, 0, _minRotationZ);
    }

    private void OnEnable()
    {
        _inputController.RotateToMax += OnRotateToMax;
        _inputController.RotateToMin += OnRotateToMin;
        _inputController.FlyUp += OnFlyUp;
    }

    private void OnDisable()
    {
        _inputController.RotateToMax -= OnRotateToMax;
        _inputController.RotateToMin -= OnRotateToMin;
        _inputController.FlyUp -= OnFlyUp;
    }

    private void Update()
    {
        Fly();
    }

    private void OnRotateToMax(bool isRotate)
    {
        _isRotateToMax = isRotate;
    }

    private void OnRotateToMin(bool isRotate)
    {
        _isRotateToMin = isRotate;
    }

    private void OnFlyUp(bool isFlyUp)
    {
        _isFlyUp = isFlyUp;
    }

    private void Fly()
    {
        if (_isRotateToMax)
            LerpRotation(_maxRotation);
        else if (_isRotateToMin)
            LerpRotation(_minRotation);

        if (_isFlyUp)
            _rigidbody2D.linearVelocity = Vector2.up * _tapForce;

        if(transform.position.x > _startPosition.x || transform.position.x < _startPosition.x)
            transform.position = new Vector2(_startPosition.x, transform.position.y);

        _rigidbody2D.angularVelocity = 0;
    }

    private void LerpRotation(Quaternion rotation)
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, _rotationSpeed * Time.deltaTime);
    }
}