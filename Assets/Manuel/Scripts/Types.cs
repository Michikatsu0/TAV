using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Types : ScriptableObject
{
    public float maxHealth = 100f;  // Salud máxima
    protected float currentHealth;

    public virtual void Initialize(GameObject obj)
    {
        currentHealth = maxHealth; // Inicializa la salud del personaje
    }

    public abstract void ExecuteBehavior(GameObject obj); // Método abstracto para el comportamiento general

    // Método para recibir daño
    public virtual void TakeDamage(GameObject obj, float damage)
    {
        currentHealth -= damage;
        Debug.Log(obj.name + " took " + damage + " damage. Current Health: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die(obj); // Llamar a morir si la salud llega a 0
        }
    }

    protected virtual void Die(GameObject obj)
    {
        Debug.Log(obj.name + " has died.");
        obj.SetActive(false); // Desactiva el objeto cuando muere
    }
}
}
