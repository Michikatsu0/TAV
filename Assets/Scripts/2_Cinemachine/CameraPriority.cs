using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPriority : MonoBehaviour
{
    public CinemachineVirtualCameraBase virtualCamera;
    public DistanceCameraSwitcher distanceCameraSwitcher;
    public bool enable;
    public CinemachineFreeLook freeLook;
    private Transform player;
    private void Start()
    {
        player = GameObject.Find("__PlayerArmature__").transform;
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject target = other.gameObject;
        if (target.CompareTag("Player"))
        {
            enable = true;
            freeLook.gameObject.SetActive(false);
            distanceCameraSwitcher.cameraSwitcher.Add(this);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        GameObject target = other.gameObject;
        if (target.CompareTag("Player"))
        {
            enable = false;
            freeLook.gameObject.SetActive(true);
            distanceCameraSwitcher.cameraSwitcher.Remove(this);
            virtualCamera.Priority = 10;
        }
    }
}
