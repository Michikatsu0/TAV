using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitBoxResponse : MonoBehaviour
{
    [HideInInspector] public PlayerHealthResponse healthResponse;

    void Start()
    {
        healthResponse = GetComponentInParent<PlayerHealthResponse>();
    }

    public void TakeHitBoxDamage(WeaponResponse weapon)
    {
        healthResponse.TakeDamage(weapon.weaponSettings.damage);
    }
}
