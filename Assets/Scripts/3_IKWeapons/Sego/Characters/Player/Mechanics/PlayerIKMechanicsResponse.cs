using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerIKMechanicsResponse : MonoBehaviour
{
    private int activeWeaponIndex;
    [SerializeField] private List<WeaponResponse> equippedWeapons = new List<WeaponResponse>();
    [SerializeField] private List<Transform> weaponSlots = new List<Transform>();
    private Transform aimRayCrossHair;
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        aimRayCrossHair = GameObject.Find("Aim_CrossHair").transform;
        animator = GetComponent<Animator>();
    }

    public void TriggerWeapon(bool isAiming)
    {
        var currentWeapon = GetWeapon(activeWeaponIndex);
        if (currentWeapon)
            currentWeapon.weaponSettings.isFiring = isAiming;
    }

    private WeaponResponse GetWeapon(int index)
    {
        if (index < 0 || index >= equippedWeapons.Count)
            return null;
        return equippedWeapons[index];
    }

    public void Equip(WeaponResponse nextWeapon)
    {
        int weaponSlotIndex = (int)nextWeapon.weaponSlot;
        var newWeapon = GetWeapon(weaponSlotIndex);
        if (newWeapon)
            Destroy(newWeapon.gameObject);
        
        newWeapon = nextWeapon;
        newWeapon.raycastDestination = aimRayCrossHair;
        newWeapon.transform.SetParent(weaponSlots[weaponSlotIndex], false);
        newWeapon.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
        equippedWeapons[weaponSlotIndex] = newWeapon;
        animator.Play(Animator.StringToHash("Weapon_" + newWeapon.weaponName), 2);
        activeWeaponIndex = weaponSlotIndex;
        //SetActiveWeapon(newWeapon.weaponSlot);
    }
}