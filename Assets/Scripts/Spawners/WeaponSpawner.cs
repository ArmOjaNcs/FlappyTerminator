using UnityEngine;

public class WeaponSpawner : SinglePrefabSpawner<EnemyWeapon>
{
    [SerializeField] private BulletSpawner _bulletSpawner;
    [SerializeField] private Pause _pause;

    public EnemyWeapon GetWeapon()
    {
        EnemyWeapon weapon = Pool.GetFreeElement();

        if( weapon != null )
            Initialize(weapon);
        
        return weapon;
    }

    public void Initialize(EnemyWeapon weapon)
    {
        weapon.gameObject.SetActive(true);

        if (weapon.IsSpawnerSubscribed == false)
        {
            _pause.Register(weapon);
            weapon.LifeTimeFinished += Release;
            weapon.SetBulletSpawner(_bulletSpawner);
            weapon.SubscribeBySpawner();
        }
    }
}