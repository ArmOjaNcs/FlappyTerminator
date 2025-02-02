using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
public class PlayerPilot : MonoBehaviour, IPauseable
{
    [SerializeField] private float _flyForce;

    private readonly float _gravity = -9.81f;

    private Vector2 _startPosition;
    private bool _isFlyUp;
    private bool _isFlyDown;
    private bool _isPaused;
    private bool _isCanFlyUp;
    private bool _isCanFlyDown;

    private void Awake()
    {
        _startPosition = transform.position;
        _isCanFlyUp = true;
        _isCanFlyDown = true;
    }

    private void Update()
    {
        if (_isPaused == false)
        {
            SaveXPosition();

            if (_isCanFlyUp && _isFlyUp)
                FlyUp();

            if (_isCanFlyDown && _isFlyDown)
                FlyDown();

            if (_isCanFlyDown)
                DoGravity();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Ceiling _))
            _isCanFlyUp = false;

        if (collision.TryGetComponent(out Ground _))
            _isCanFlyDown = false;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Ceiling _))
            _isCanFlyUp = true;

        if (collision.TryGetComponent(out Ground _))
            _isCanFlyDown = true;
    }

    public void Stop()
    {
        _isPaused = true;
    }

    public void Resume()
    {
        _isPaused = false;
    }

    public void FlyUp(bool isFlyUp)
    {
        _isFlyUp = isFlyUp;
    }

    public void FlyDown(bool isFlyDown)
    {
        _isFlyDown = isFlyDown;
    }

    private void FlyUp()
    {
        if (_isFlyUp)
            transform.Translate(Vector2.up * _flyForce * Time.deltaTime);
    }

    private void FlyDown()
    {
        if (_isFlyDown)
            transform.Translate(Vector2.up * -_flyForce / 2 * Time.deltaTime);
    }

    private void SaveXPosition()
    {
        if (transform.position.x > _startPosition.x || transform.position.x < _startPosition.x)
            transform.position = new Vector2(_startPosition.x, transform.position.y);
    }

    private void DoGravity()
    {
        transform.Translate(Vector2.up * _gravity * Time.deltaTime);
    }
}