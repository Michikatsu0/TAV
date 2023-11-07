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
    [HideInInspector] public WeaponResponse currentWeapon;
    private Animator animator;
    private PlayerHealthResponse healthResponse;
    // Start is called before the first frame update
    void Start()
    {
        healthResponse = GetComponent<PlayerHealthResponse>();
        aimRayCrossHair = GameObject.Find("Aim_CrossHair").transform;
        animator = GetComponent<Animator>();
    }

    public void TriggerWeapon(bool isAiming)
    {
        currentWeapon = GetWeapon(activeWeaponIndex);

        if (!healthResponse.deathScript)
            if (currentWeapon)
                PlayerActionsResponse.ActionShootWeaponTrigger?.Invoke(isAiming, currentWeapon);
        else
            PlayerActionsResponse.ActionShootWeaponTrigger?.Invoke(false, currentWeapon);
        
        
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
