using System.Collections.Generic; // Importar para usar List
using UnityEngine;
using UnityEngine.UI;

public class SecurityCamera : MonoBehaviour
{
    [SerializeField] private StatsCamEnemy settings;
    [SerializeField] private Transform player;
    [SerializeField] private Slider detectionSlider;
    [SerializeField] private Image fillImage;
    [SerializeField] private Transform[] patrolPoints;
    [SerializeField] private Transform movingTransform, rotatingTransform;
    [SerializeField] private List<AIEnemyRobotHumanoid> enemyRobots; // Referencia a los scripts de los enemigos

    private float detectionTimer = 0f;
    private int alertLevel = 0;
    private Transform mainCam;
    private float alertCooldownTimer = 0f;
    private int currentPatrolPoint = 0;

    void Start()
    {
        mainCam = Camera.main.transform;
        SetCameraState(0);
        detectionSlider.maxValue = settings.detectionTime;
        detectionSlider.value = 0;
    }

    void FixedUpdate()
    {
        Debug.Log("Enfriamiento: " + alertCooldownTimer);
        detectionSlider.transform.rotation = mainCam.rotation;

        float distancePlayer = Vector3.Distance(movingTransform.position, player.position);

        if (alertLevel != 2)
        {
            if (distancePlayer >= settings.detectionRange && alertLevel > 0)
            {
                detectionTimer -= Time.deltaTime;

                if (detectionTimer <= 0)
                {
                    alertCooldownTimer = 0;
                    SetCameraState(0);
                    detectionTimer = 0f;
                    detectionSlider.value = 0f;
                }
            }
        }
        else if (alertLevel == 2)
        {
            foreach (AIEnemyRobotHumanoid enemyRobot in enemyRobots)
            {
                enemyRobot.FollowPlayer();
            }
            
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
        movingTransform.position = Vector3.MoveTowards(movingTransform.position, targetPoint.position, settings.movementSpeed * Time.deltaTime);

        Quaternion lookRotation = Quaternion.LookRotation(direction);
        rotatingTransform.rotation = Quaternion.Slerp(rotatingTransform.rotation, lookRotation, settings.rotationSpeed * Time.deltaTime);

        if (Vector3.Distance(movingTransform.position, targetPoint.position) < 0.1f)
        {
            currentPatrolPoint = (currentPatrolPoint + 1) % patrolPoints.Length;
        }
    }

    void FollowPlayer()
    {
        // La cámara sigue al jugador
        movingTransform.position = player.position;
        // Todos los enemigos siguen al jugador
        foreach (AIEnemyRobotHumanoid enemyRobot in enemyRobots)
        {
            enemyRobot.FollowPlayer();
        }
    }

    void DetectPlayer()
    {
        float distanceToPlayer = Vector3.Distance(movingTransform.position, player.position);

        if (distanceToPlayer <= settings.detectionRange)
        {
            detectionTimer += Time.deltaTime;
            if (detectionTimer >= settings.detectionTime)
            {
                if (alertLevel == 0)
                {
                    SetCameraState(1);
                }
                else if (alertLevel == 1)
                {
                    SetCameraState(2);
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

    void SetCameraState(int level)
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
        Color detectionBarColor = settings.ColorG.Evaluate(detectionSlider.value / detectionSlider.maxValue);
        fillImage.color = detectionBarColor;
    }
}
