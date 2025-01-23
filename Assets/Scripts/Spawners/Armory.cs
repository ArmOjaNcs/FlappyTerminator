using System.Collections.Generic;
using UnityEngine;

public class Armory : MonoBehaviour
{
    [SerializeField] private List<WeaponSpawner> _spawners; 

    public EnemyWeapon GetRandomWeapon()
    {
        int randomIndex = Random.Range(0, _spawners.Count);
        return _spawners[randomIndex].GetWeapon();
    }
}