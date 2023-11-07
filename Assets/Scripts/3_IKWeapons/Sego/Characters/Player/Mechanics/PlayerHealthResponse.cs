using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using Unity.Burst.CompilerServices;
/// <summary>
/// changed the max health multiplying it with the modifier
/// also changed the regen speed and regen time
/// </summary>

public class PlayerHealthResponse : MonoBehaviour
{
    [SerializeField] private StatsSettings statsSettings;
    [HideInInspector] public RagdollResponse ragdoll;
    public float currentHealth;
    [HideInInspector] public bool deathScript, onHit;

    
    private SkinnedMeshRenderer skinnedMeshRenderer;
    private CharacterController characterController;
    private PlayerIKMechanicsResponse playerIKMechanics;
    private Slider healthSlider;
    private Image fillImage, playerImage;
    private TextMeshProUGUI tMPHealth;

    private float blinkTimer, intensity, tmpTimeToRegenerate, regenerateValue = 0;
    private bool canRegenerate = true, isRegenerating;
    
    private void Start()
    {
        var rgbds = GetComponentsInChildren<Rigidbody>();
        foreach (var rgbd in rgbds)
        {
            PlayerHitBoxResponse hitbox = rgbd.AddComponent<PlayerHitBoxResponse>();
            hitbox.healthResponse = this;
        }

        skinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();

        skinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();

        healthSlider = GameObject.Find("Health_Bar").GetComponent<Slider>();

        fillImage = healthSlider.fillRect.gameObject.GetComponent<Image>();

        playerImage = GameObject.Find("Player_Image").GetComponent<Image>();

        tMPHealth = healthSlider.gameObject.GetComponentInChildren<TextMeshProUGUI>();

        ragdoll = GetComponentInChildren<RagdollResponse>();
        characterController = GetComponent<CharacterController>();
        playerIKMechanics = GetComponent<PlayerIKMechanicsResponse>();
        currentHealth = statsSettings.maxHealth; //max health changed

        healthSlider.maxValue = statsSettings.maxHealth; //here too
        healthSlider.value = currentHealth;

    }

    private void Update()
    {

        blinkTimer -= Time.deltaTime;

        intensity = (Mathf.Clamp01(blinkTimer / statsSettings.blinkDuration) * statsSettings.blinkIntensity) + 1.0f;

        if (intensity == 1 && !isRegenerating)
            playerImage.color = Color.white * intensity;
        else
            playerImage.color = Color.red * intensity;

        if (blinkTimer > -1f)
            BlinkColorChanger();

        healthSlider.value = Mathf.Lerp(healthSlider.value, currentHealth, statsSettings.transitionDamageLerp * Time.deltaTime);
        int  stringHealth = (int)healthSlider.value;
        tMPHealth.text = stringHealth.ToString();

        if (deathScript) return;

        ColorChanger();
        IsDeath();

        if (currentHealth < 10.0f && !isRegenerating && canRegenerate)
        {
            Invoke(nameof(DelayRegeneration), tmpTimeToRegenerate);
            canRegenerate = false;
        }

        if (isRegenerating)
        {
            playerImage.color = Color.green * 1;
            regenerateValue += Time.deltaTime; 
            currentHealth = regenerateValue * statsSettings.regenerationSpeed; //regeneration speed changed

            if (healthSlider.value >= 10.0f)
            {
                canRegenerate = true;
                isRegenerating = false;
                regenerateValue = 0;
                tmpTimeToRegenerate = statsSettings.timeToRegenerate; //here we will change the time to regenerate
            }
        }
    }

    void BlinkColorChanger()
    {
        skinnedMeshRenderer.materials[0].color = statsSettings.armatureColorsMaterial[0] * intensity;
        skinnedMeshRenderer.materials[1].color = statsSettings.armatureColorsMaterial[1] * intensity;
        skinnedMeshRenderer.materials[2].color = statsSettings.armatureColorsMaterial[2] * intensity;

        foreach (var material in statsSettings.armatureHelmetMaterials)
            material.color = Color.white * intensity;
    }

    void DelayRegeneration()
    {
        isRegenerating = true;
        regenerateValue = currentHealth;
    }

    public void TakeDamage(float amount) //Changes the current Health, public so enemydamage can access it. When damaged, starts the timer for invencibility
    {
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, statsSettings.maxHealth);
        blinkTimer = statsSettings.blinkDuration;
        isRegenerating = false;
        canRegenerate = true;
        StartCoroutine(Disable());
    }
    IEnumerator Disable()
    {
        onHit = true;
        yield return new WaitForSeconds(0.015f);
        onHit = false;
    }
    void ColorChanger()
    {
        Color healthBarColor = Color.Lerp(statsSettings.sliderColors[1], statsSettings.sliderColors[0], healthSlider.value / healthSlider.maxValue);
        fillImage.color = healthBarColor;
    }

    public void IsDeath()
    {

        if (currentHealth <= 0.0f)
            StartCoroutine(DeathCoroutine());

        if (transform.position.y <= -20)
            currentHealth = 0;
    }

    IEnumerator DeathCoroutine() //waits for the destruction of the player, use and adjust the time for a death animation
    {
        deathScript = true;
        ragdoll.ActivateRagdolls();
            
        PlayerActionsResponse.ActionWeaponDeath?.Invoke(true);
        characterController.enabled = false;
        yield return new WaitForSeconds(statsSettings.deathTime);
    }

}
