using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Base Data Cam Stats", order = 1)]
public class CameraSettings : ScriptableObject, ISerializationCallbackReceiver
{
    [Header("Detection Settings")]
    [SerializeField] public float detectionRange;
    [SerializeField] public float detectionTime;
    [Header("Lens Colors")]
    [SerializeField] public Color patrolColor;
    [SerializeField] public Color alertColor;
    [SerializeField] public Color attackColor;
    [Header("Alert Settings")]
    [SerializeField] public float alertDuration;
    [SerializeField] public float transitionDamageLerp;

    public void Init() { }

    public void OnBeforeSerialize()
    {
        Init();
    }

    public void OnAfterDeserialize() { }
}
