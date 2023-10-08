using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class DistanceCameraSwitcher : MonoBehaviour
{
    public List<CameraPriority> cameraSwitcher = new List<CameraPriority>();

    private void Update()
    {
        foreach (var cameraSwitcher in cameraSwitcher)
        {
            if (cameraSwitcher.enable)
            {
                cameraSwitcher.virtualCamera.Priority = 20;
            }
        }
    }

}
