using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class HealthEnemyResponse : MonoBehaviour
{
    [SerializeField] public StatsEnemySettings statsEnemySettings;
    [SerializeField] private Slider healthSlider;
    [SerializeField] public bool humanoide;
    private SkinnedMeshRenderer[] skinnedMeshRenderer;
    private AudioSource audioSource;
    private Image fillImage;
    [HideInInspector] public float currentHealth, maxHealth;
    private float blinkTimer, intensity;
    [HideInInspector] public bool deathScript, onHit;
    private Transform cam;
    [HideInInspector] public RagdollResponse ragdoll;

    void Start()
    {
        cam = GameObject.Find("__MainCamera__").transform;
        if (humanoide)
        {
            var rgbds = GetComponentsInChildren<Rigidbody>();
            foreach(var rgbd in rgbds)
            {
                EnemyHitboxResponse hitbox = rgbd.AddComponent<EnemyHitboxResponse>();
                hitbox.healthEnemy = this;
            }
            skinnedMeshRenderer = GetComponentsInChildren<SkinnedMeshRenderer>();
        }

        audioSource = GetComponent<AudioSource>();
        maxHealth = statsEnemySettings.maxHealth;
        currentHealth = maxHealth;
        healthSlider.maxValue= maxHealth;
        healthSlider.minValue = 0;
        healthSlider.value = maxHealth;
        fillImage = healthSlider.fillRect.gameObject.GetComponent<Image>();
        ragdoll = GetComponentInChildren<RagdollResponse>();
    }

    void Update()
    {
        healthSlider.transform. rotation = cam.transform.rotation;

        blinkTimer -= Time.deltaTime;
        intensity = (Mathf.Clamp01(blinkTimer / statsEnemySettings.blinkDuration) * statsEnemySettings.blinkIntensity) + 1.0f;
        if (blinkTimer > -1f)
            BlinkColorChanger();
        healthSlider.value = Mathf.Lerp(healthSlider.value, currentHealth, statsEnemySettings.transitionDamageLerp * Time.deltaTime);
        if (deathScript) return;
        IsDeath();
        UIColorChanger();
    }

    public void TakeDamage(float amount) 
    {
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        blinkTimer = statsEnemySettings.blinkDuration;
        onHit = true;
    }

    void BlinkColorChanger()
    {
        if (humanoide)
        {
            skinnedMeshRenderer[0].materials[0].color = statsEnemySettings.armatureColorMaterials[0] * intensity;
            skinnedMeshRenderer[1].materials[0].color = statsEnemySettings.armatureColorMaterials[1] * intensity;
        }
        else
            statsEnemySettings.effectMaterial[0].color = Color.white * intensity;
    }

    void UIColorChanger()
    {
        Color healthBarColor = Color.Lerp(statsEnemySettings.sliderColors[1], statsEnemySettings.sliderColors[0], healthSlider.value / healthSlider.maxValue);
        fillImage.color = healthBarColor;
    }

    public void IsDeath()
    {
        if (currentHealth <= 0.0f)
            StartCoroutine(DeathCoroutine());
    }

    IEnumerator DeathCoroutine() 
    {
        deathScript = true;
        audioSource.PlayOneShot(statsEnemySettings.deathClips[Random.Range(0, 4)], 1f);

        ragdoll.ActivateRagdolls();

        yield return new WaitForSeconds(statsEnemySettings.deathTime);
        healthSlider.gameObject.SetActive(false);
    }
}
