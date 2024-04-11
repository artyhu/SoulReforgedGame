using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthSystem : MonoBehaviour
{
    public static event Action<EnemyHealthSystem> OnEnemyKilled;
    public float health, maxHealth;
    public GameObject soulFrag;
    private string id;

    [SerializeField]
    private AllowExit door;

    public GameObject floatingHP;
    public GameObject floatingHPf;

    // Start is called before the first frame update
    void Awake()
    {
        health = maxHealth;
        id = gameObject.tag;
    }

    public void TakeDamage(float damageAmount)
    {
        // play some hurt animation here

        Debug.Log($"Damage: {damageAmount}");
        health -= damageAmount;
        Debug.Log($"Health: {health}");

        if (damageAmount == 3)
        {
            Instantiate(floatingHP, transform.position, Quaternion.identity);
        }
        else if (damageAmount == 5)
        {
            Instantiate(floatingHPf, transform.position, Quaternion.identity);
        }

        if (health <= 0)
        {
            Destroy(gameObject);
            if (soulFrag != null)
            {
                Instantiate(soulFrag, gameObject.transform.position, Quaternion.identity);
            }
            if (gameObject.tag == "Mage")
            {
                door.Activate();
            }
            Instantiate(floatingHP, transform.position, Quaternion.identity);
            OnEnemyKilled?.Invoke(this);
        }
    

    }
}
