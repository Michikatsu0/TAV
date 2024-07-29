using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Base Data Cam Stats", order = 1)]
public class StatsCamEnemy : ScriptableObject, ISerializationCallbackReceiver
{
    [Header("Detection Settings")]
    [SerializeField] public float detectionRange;
    [SerializeField] public float detectionTime;

    [Header("Lens Colors")]
    [SerializeField] public Gradient ColorG;

    [Header("Alert Settings")]
    [SerializeField] public float alertDuration;
    [SerializeField] public float transitionDamageLerp;

    [Header("Movement Settings")]
    [SerializeField] public float rotationSpeed = 1f;
    [SerializeField] public float movementSpeed = 2f;

    public void Init() { }

    public void OnBeforeSerialize()
    {
        Init();
    }

    public void OnAfterDeserialize() { }
}
