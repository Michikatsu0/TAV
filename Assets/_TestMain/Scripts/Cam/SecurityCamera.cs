using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.SceneView;

public class SecurityCamera : MonoBehaviour
{
    [SerializeField] private StatsCamEnemy settings;
    [SerializeField] private Transform player;
    [SerializeField] private Renderer cameraLens;
    [SerializeField] private Slider detectionSlider;

    private float detectionTimer = 0f;
    private int alertLevel = 0;
    private Image fillImage;
    private Transform cam;
    private float alertCooldownTimer = 0f;

    void Start()
    {
        cam = Camera.main.transform;
        SetCameraState(settings.patrolColor, 0);
        detectionSlider.maxValue = settings.detectionTime;
        detectionSlider.value = 0;
        fillImage = detectionSlider.fillRect.GetComponent<Image>();
    }

    void Update()
    {
        detectionSlider.transform.rotation = cam.rotation;

        if (alertLevel > 0)
        {
            alertCooldownTimer -= Time.deltaTime;
            if (alertCooldownTimer <= 0)
            {
                SetCameraState(settings.patrolColor, 0);
                detectionTimer = 0f;
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
                SetCameraState(settings.alertColor, 1);
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
    }

    void SetCameraState(Color lensColor, int level)
    {
        cameraLens.material.color = lensColor;
        alertLevel = level;
        alertCooldownTimer = settings.alertDuration;

        // Additional logic for different alert levels
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
        // Logic to alert enemies
        // For example, you can find nearby enemies and set them to patrol the area
        yield return new WaitForSeconds(1f); // Placeholder delay
    }
}
