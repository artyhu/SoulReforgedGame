using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageCombat : MonoBehaviour
{

    [SerializeField]
    private GameObject fireball, hand, mine;
    private Animator animator;
    private EnemyHealthSystem health;
    public bool invulnerable { get; set; }

    private void Awake()
    {
        animator = GetComponent<Animator>();
        health = GetComponent<EnemyHealthSystem>();
        invulnerable = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }


    public void Attack(int type, Transform target)
    {
        GameObject spell;

        if (type == 0)
        {
            spell = Instantiate(fireball, transform.position, Quaternion.identity);
            spell.GetComponent<ExplosiveSpells>().target = target;
        }

        else if (type == 1)
        {
            spell = Instantiate(hand, transform.position, Quaternion.identity);
            spell.GetComponent<Hand>().target = target;
        }

        else
        {
            spell = Instantiate(mine, transform.position, Quaternion.identity);
            spell.GetComponent<ExplosiveSpells>().target = target;
        }


    }
    
    public void resetHp()
    {
        health.health = health.maxHealth;
    }
}
