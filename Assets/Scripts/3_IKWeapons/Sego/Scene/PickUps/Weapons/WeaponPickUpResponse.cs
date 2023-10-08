using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickUpResponse : MonoBehaviour
{
    [SerializeField] private WeaponResponse weaponPrefab;
    private void OnTriggerEnter(Collider other)
    {
        GameObject target = other.gameObject;
        if (target.CompareTag("Player"))
        {
            PlayerIKMechanicsResponse player = target.GetComponent<PlayerIKMechanicsResponse>();
            WeaponResponse weapon = Instantiate(weaponPrefab);
            player.Equip(weapon);
        }
    }
}
