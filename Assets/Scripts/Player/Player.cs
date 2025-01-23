using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerWeapon _weapon;
    [SerializeField] private BulletSpawner _bulletSpawner;

    private void Awake()
    {
        _weapon.SetBulletSpawner(_bulletSpawner);
    }
}