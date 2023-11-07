using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class HumanoidAttack : MonoBehaviour
{
    [HideInInspector] public Vector3 attackDirection;
    [HideInInspector] public Collider hitBox;
    [HideInInspector] public float attackDistance, angularSpeed, damage;
    [HideInInspector] public bool stopAttack;
    void Start()
    {
        hitBox = GetComponent<Collider>();
        hitBox.isTrigger = true;
        hitBox.enabled = false;
    }

    void HitBoxAttack(Collider other, PlayerHitBoxResponse playerHitBox)
    {
        playerHitBox.TakeHitBoxDamage((int)damage);
        if (playerHitBox.healthResponse.currentHealth <= 0.0f)
        {
            var rgbd = playerHitBox.GetComponent<Rigidbody>();
            if (rgbd != null)
            {
                Vector3 contactPoint = other.ClosestPointOnBounds(transform.position);
                rgbd.AddForceAtPosition(Vector3.forward * 1.5f, contactPoint, ForceMode.Impulse);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject target = other.gameObject;

        var playerHitBox = target.GetComponentInChildren<PlayerHitBoxResponse>();

        if (playerHitBox)
        {
            HitBoxAttack(other, playerHitBox);
            var healthPlayer = playerHitBox.GetComponentInParent<PlayerHealthResponse>();
            if (healthPlayer.currentHealth <= 0.0f)
            {
                stopAttack = true;
            }
        }
    }

}
