using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveSpells : MonoBehaviour
{
    private LayerMask enemyLayers;

    PlayerHealth player;

    private Rigidbody2D rb;
    private Animator animator;

    [SerializeField] private Transform attackPos, triggerPos;
    public Transform target { get; set; }

    [SerializeField]
    private float speed, dmgRangeX, dmgRangeY, spellDmg;
    private bool exploded = false;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!exploded)
        {
            rb.position = Vector2.MoveTowards(rb.position, new Vector2(target.position.x, target.position.y + 0.6f), speed * Time.deltaTime);
            
            if(gameObject.tag == "FireSpell")
            {
                Vector3 direction = (target.position - transform.position).normalized;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                rb.rotation = angle;
            }
            /*
            Vector3 direction = (target.position - transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            rb.rotation = angle;
            */
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "Player" || collision.tag == "Clone")
        {
            animator.SetTrigger("Explode");
            exploded = true;

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

            Destroy(gameObject, 2f);
        }

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(attackPos.position, new Vector3(dmgRangeX, dmgRangeY, 0));
    }
}
