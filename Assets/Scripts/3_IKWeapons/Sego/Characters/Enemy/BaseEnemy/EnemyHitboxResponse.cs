using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitboxResponse : MonoBehaviour
{
    [HideInInspector] public HealthEnemyResponse healthEnemy;

    // Start is called before the first frame update
    void Start()
    {
        healthEnemy = GetComponentInParent<HealthEnemyResponse>();
    }

    public void TakeHitBoxDamage(WeaponResponse weapon)
    {
        healthEnemy.TakeDamage(weapon.weaponSettings.damage);
    }

}
