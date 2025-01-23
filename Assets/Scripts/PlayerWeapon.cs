using UnityEngine;

public class PlayerWeapon : Shooter
{
    [SerializeField] private InputController _inputController;

    private void OnEnable()
    {
        _inputController.Shoot += SetIsCanShoot;
    }

    private void OnDisable()
    {
        _inputController.Shoot -= SetIsCanShoot;
    }

    private protected override void Update()
    {
        if (IsTimeToShoot() && IsCanShoot)
        {
            SetDirection(GetDirection());
            Shoot();
        }
    }

    private Vector2 GetDirection()
    {
        return FirePoint.position - transform.position;
    }
}