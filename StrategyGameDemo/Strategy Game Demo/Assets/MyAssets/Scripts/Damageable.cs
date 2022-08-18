using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class Damageable: MonoBehaviour
{
    protected int health { get; set; }
    protected int maxHealth { get; set; }

    public void TakeDamage(int damage)
    {
        if (health >= damage)
        {
            health -= damage;
        }
        else
        {
            health = 0; 
        }
    }

    public int GetHealth()
    {
        return health;
    }

    public int GetMaxHealth()
    {
        return maxHealth;
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}