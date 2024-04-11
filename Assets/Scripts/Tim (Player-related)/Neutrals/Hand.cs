using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    private LayerMask enemyLayers;

    PlayerHealth player;

    private Rigidbody2D rb;
    private Animator animator;

    [SerializeField] private Transform attackPos, triggerPos;
    public Transform target { get; set; }

    [SerializeField]
    private float speed, triggerDistance, dmgRangeX, dmgRangeY, spellDmg;
    private bool handAttack = true, attacking = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (target == null)
        {
            FindTarget();
        }

        else
        {
            if (Vector2.Distance(triggerPos.position, target.position) > triggerDistance && !attacking)
            {
                Vector3 scale = transform.localScale;
                if (target.position.x < transform.position.x)
                {
                    scale.x = Mathf.Abs(scale.x) * -1;
                }
                else
                {
                    scale.x = Mathf.Abs(scale.x);
                }
                transform.localScale = scale;

                rb.position = Vector2.MoveTowards(rb.position, target.position, speed * Time.deltaTime);
            }
            else if (handAttack)
            {
                HandAttack();
                StartCoroutine(handCD());
            }
        }
    }

    private void HandAttack()
    {
        animator.SetTrigger("Attack");
        handAttack = false;
        attacking = true;
        
        Collider2D[] hits = Physics2D.OverlapBoxAll(attackPos.position, new Vector2(dmgRangeX, dmgRangeY), enemyLayers);
        foreach (Collider2D obj in hits)
        {
            if (obj.tag == "Player")
            {
                player.TakeDamage(spellDmg, true);
            }
            else if (obj.tag == "Clone")
            {
                player.TakeDamage(spellDmg);
            }
        }
        
    }


    private void FindTarget()
    {

    }

    private IEnumerator handCD()
    {
        yield return new WaitForSeconds(0.7f);
        attacking = false;
        yield return new WaitForSeconds(2f);
        handAttack = true;
        
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(attackPos.position, new Vector3(dmgRangeX, dmgRangeY, 0));
        Gizmos.DrawWireSphere(triggerPos.position, triggerDistance);
    }
}
