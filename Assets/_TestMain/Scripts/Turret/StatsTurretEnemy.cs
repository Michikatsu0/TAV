using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Base Data Turret Stats", order = 1)]
public class StatsTurretEnemy : ScriptableObject, ISerializationCallbackReceiver
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

    [Header("Attack Settings")]
    [SerializeField] public float attackCooldown;
    [SerializeField] public float attackDamage;

    public void Init() { }

    public void OnBeforeSerialize()
    {
        Init();
    }

    public void OnAfterDeserialize() { }
}
