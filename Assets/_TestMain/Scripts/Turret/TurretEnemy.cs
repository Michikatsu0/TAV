using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurretEnemy : MonoBehaviour
{
    [SerializeField] private StatsTurretEnemy settings;
    [SerializeField] private Transform player;
    [SerializeField] private Slider detectionSlider;
    [SerializeField] private Image fillImage;
    [SerializeField] private Transform[] patrolPoints;
    [SerializeField] private Transform movingTransform, rotatingTransform;
    [SerializeField] private TurretWeapon turretWeapon;

    private float detectionTimer = 0f;
    private int alertLevel = 0;
    private Transform mainCam;
    private float alertCooldownTimer = 0f;
    private int currentPatrolPoint = 0;

    void Start()
    {
        mainCam = Camera.main.transform;
        SetTurretState(0);
        detectionSlider.maxValue = settings.detectionTime;
        detectionSlider.value = 0;
    }

    void FixedUpdate()
    {
        detectionSlider.transform.rotation = mainCam.rotation;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (alertLevel != 2)
        {
            if (distanceToPlayer >= settings.detectionRange && alertLevel > 0)
            {
                detectionTimer -= Time.deltaTime;

                if (detectionTimer <= 0)
                {
                    alertCooldownTimer = 0;
                    SetTurretState(0);
                    detectionTimer = 0f;
                    detectionSlider.value = 0f;
                }
            }
        }
        else if (alertLevel == 2)
        {
            
            turretWeapon.AttackPlayer(player);
        }

        if (alertLevel == 0)
        {
            Patrol();
        }

        DetectPlayer();
        UpdateDetectionSlider();
    }

    void Patrol()
    {
        Transform targetPoint = patrolPoints[currentPatrolPoint];
        Vector3 direction = (targetPoint.position - movingTransform.position).normalized;
        movingTransform.position = Vector3.MoveTowards(movingTransform.position, targetPoint.position, settings.rotationSpeed * Time.deltaTime);

        Quaternion lookRotation = Quaternion.LookRotation(direction);
        rotatingTransform.rotation = Quaternion.Slerp(rotatingTransform.rotation, lookRotation, settings.rotationSpeed * Time.deltaTime);

        if (Vector3.Distance(movingTransform.position, targetPoint.position) < 0.1f)
        {
            currentPatrolPoint = (currentPatrolPoint + 1) % patrolPoints.Length;
        }
    }

    void DetectPlayer()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        Debug.Log("Distance to player: " + distanceToPlayer);

        if (distanceToPlayer <= settings.detectionRange)
        {
            detectionTimer += Time.deltaTime;
            Debug.Log("Detection Timer: " + detectionTimer);
            if (detectionTimer >= settings.detectionTime)
            {
                if (alertLevel == 0)
                {
                    SetTurretState(1);
                }
                else if (alertLevel == 1)
                {
                    SetTurretState(2);
                }
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

    void SetTurretState(int level)
    {
        alertLevel = level;
        alertCooldownTimer = settings.alertDuration;

        switch (alertLevel)
        {
            case 0:
                detectionSlider.value = 0;
                detectionTimer = 0;
                break;
            case 1:
                break;
            case 2:
                break;
        }
    }

    void UIColorChanger()
    {
        float normalizedValue = detectionSlider.value / detectionSlider.maxValue;

        if (float.IsNaN(normalizedValue) || float.IsInfinity(normalizedValue) || normalizedValue < 0 || normalizedValue > 1)
        {
            normalizedValue = 0;
        }

        Color detectionBarColor = settings.ColorG.Evaluate(normalizedValue);
        fillImage.color = detectionBarColor;
    }
}
