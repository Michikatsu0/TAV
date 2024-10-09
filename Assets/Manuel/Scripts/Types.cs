using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Types : ScriptableObject
{
public float maxHealth = 100f;  // Salud máxima
    protected float currentHealth;

    // Nuevo: Referencia al modelo 3D o Prefab del personaje
    public GameObject characterModelPrefab;  

    // Guardar la referencia al objeto instanciado
    protected GameObject instantiatedModel;
    public Vector3 move;

    // Método para inicializar el personaje
    

    public abstract void ExecuteBehavior(GameObject obj); // Método abstracto para el comportamiento general
public abstract void Inicialize(GameObject obj);
    // Método para recibir daño
    public virtual void TakeDamage(GameObject obj, float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die(obj); // Llamar a morir si la salud llega a 0
        }
    }

    // Método para morir
    protected virtual void Die(GameObject obj)
    {
     

        obj.SetActive(false); // Desactiva el objeto controlador
    }
}
