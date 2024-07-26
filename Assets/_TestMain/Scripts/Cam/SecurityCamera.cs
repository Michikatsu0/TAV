using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SecurityCamera : MonoBehaviour
{
    [SerializeField] private StatsCamEnemy settings;
    [SerializeField] private Transform player;
    [SerializeField] private Slider detectionSlider;
    [SerializeField] private Image fillImage;

    private float detectionTimer = 0f;
    private int alertLevel = 0;
    private Transform MainCam;
    private float alertCooldownTimer = 0f;

    void Start()
    {
        MainCam = Camera.main.transform;
        SetCameraState(0);
        detectionSlider.maxValue = settings.detectionTime;
        detectionSlider.value = 0;
    }

    void Update()
    {
        detectionSlider.transform.rotation = MainCam.rotation;

        if (alertLevel > 0)
        {
            alertCooldownTimer -= Time.deltaTime;
            if (alertCooldownTimer <= 0)
            {
                SetCameraState(0);
                detectionTimer = 0f;
                detectionSlider.value = 0f;
            }
        }

        DetectPlayer();
        UpdateDetectionSlider();
    }

    void DetectPlayer()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= settings.detectionRange)
        {
            detectionTimer += Time.deltaTime;
            if (detectionTimer >= settings.detectionTime && alertLevel == 0)
            {
                SetCameraState(1);
            }
        }
        else
        {
            detectionTimer = Mathf.Max(0, detectionTimer - Time.deltaTime);
        }
    }

    void UpdateDetectionSlider()
    {
        detectionSlider.value = Mathf.Lerp(detectionSlider.value, detectionTimer, settings.transitionDamageLerp * Time.deltaTime);
        UIColorChanger();
    }

    void SetCameraState(int level)
    {
        alertLevel = level;
        alertCooldownTimer = settings.alertDuration;

        if (alertLevel == 1)
        {
            StartCoroutine(AlertEnemies());
        }
        else if (alertLevel == 0)
        {
            detectionSlider.value = 0;
        }
    }

    IEnumerator AlertEnemies()
    {
        // Aquí lógica para encontrar enemigos cercanos y hacer que patrullen la zona
        yield return new WaitForSeconds(1f); // Temporizador de ejemplo
    }

    void UIColorChanger()
    {
        Color detectionBarColor = settings.ColorG.Evaluate(detectionSlider.value / detectionSlider.maxValue);
        fillImage.color = detectionBarColor;
    }
}
