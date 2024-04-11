using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingSpells : MonoBehaviour
{
    [SerializeField]
    private float health, maxHealth;

    private void Awake()
    {
        health = maxHealth;
    }

    private void FixedUpdate()
    {
           
    }

    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount;
        // play particle effect;

        if (health <= 0)
        {
            // play destroyed animation
            Destroy(gameObject);
        }
    }
}
