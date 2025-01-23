using UnityEngine;

public class WeaponSpawner : SinglePrefabSpawner<EnemyWeapon>
{
    [SerializeField] private BulletSpawner _bulletSpawner;

    public EnemyWeapon GetWeapon()
    {
        EnemyWeapon weapon = Pool.GetFreeElement();
        Initialize(weapon);
        return weapon;
    }

    public void Initialize(EnemyWeapon weapon)
    {
        weapon.gameObject.SetActive(true);

        if (weapon.IsSpawnerSubscribed == false)
        {
            weapon.LifeTimeFinished += Release;
            weapon.SetBulletSpawner(_bulletSpawner);
            weapon.SubscribeBySpawner();
        }
    }
}