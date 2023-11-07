using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerAiming : MonoBehaviour
{
    [SerializeField] private Transform cameraLookAt;
    [SerializeField] private AxisState xAxis;
    [SerializeField] private AxisState yAxis;
    private Vector3 lookCameraDirection;

    private Camera mainCamera;
    private CharacterController characterController;
    private PlayerHealthResponse healthResponse;
    void Start()
    {
        mainCamera = Camera.main;
        characterController = GetComponent<CharacterController>();
        healthResponse = GetComponent<PlayerHealthResponse>();
        Cursor.lockState = CursorLockMode.Locked; // Bloquea el cursor en el centro de la pantalla
        Cursor.visible = false; // Oculta el cursor
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (healthResponse.deathScript) return;

        xAxis.Update(Time.fixedDeltaTime);
        yAxis.Update(Time.fixedDeltaTime);

        lookCameraDirection.x = yAxis.Value;
        lookCameraDirection.y = xAxis.Value;
        
        cameraLookAt.eulerAngles = lookCameraDirection;
    }
}
