using System.Collections.Generic;
using UnityEngine;

public class TurretWeapon : MonoBehaviour
{
    [SerializeField] private WeaponSettings weaponSettings;
    [SerializeField] private ParticleSystem[] muzzleEffects;
    [SerializeField] private Transform raycastOrigin;

    private List<WeaponResponse.Bullet> bullets = new List<WeaponResponse.Bullet>();
    private float attackCooldownTimer = 0f;

    public void AttackPlayer(Transform player)
    {
        transform.LookAt(player);

        if (attackCooldownTimer <= 0f)
        {
            FireBullet(player);
            attackCooldownTimer = weaponSettings.fireRate;
        }
        else
        {
            attackCooldownTimer -= Time.deltaTime;
        }
    }

    private void FireBullet(Transform player)
    {
        foreach (var particleSystem in muzzleEffects)
            particleSystem.Emit(1);

        Vector3 velocity = (player.position - raycastOrigin.position).normalized * weaponSettings.bulletSpeed;
        var bullet = CreateBullet(raycastOrigin.position, velocity);
        bullets.Add(bullet);

        var randomShootClip = Random.Range(0, weaponSettings.weaponShootAudioClips.Count);
        GetComponent<AudioSource>().PlayOneShot(weaponSettings.weaponShootAudioClips[randomShootClip], 0.5f);
    }

    private WeaponResponse.Bullet CreateBullet(Vector3 position, Vector3 velocity)
    {
        WeaponResponse.Bullet bullet = new WeaponResponse.Bullet();
        bullet.initialPosition = position;
        bullet.initialVelocity = velocity;
        bullet.time = 0.0f;
        bullet.tracer = Instantiate(weaponSettings.tracerEffect, position, Quaternion.identity);
        bullet.tracer.material.SetColor("_EmissionColor", weaponSettings.colorMuzzle);
        bullet.tracer.AddPosition(position);
        bullet.bounce = weaponSettings.maxNumberBounces;
        return bullet;
    }
}
