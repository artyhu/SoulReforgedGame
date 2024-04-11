using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    private bool exploded = false;

    [SerializeField]
    private float fireDamage = 3f;
    [SerializeField]
    private float explosionDamage = 5f;
    private float range;

    private Animator animator;
    private Rigidbody2D rb;
    [SerializeField]
    private Transform firePoint;
    [SerializeField]
    private Transform explosionPoint;
    [SerializeField]
    private LayerMask enemyLayers;
    private AudioSource source;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        range = 0.3f;
        source = GetComponent<AudioSource>();
    }

    private void Start()
    {
        StartCoroutine(LifeTime(1));
    }

    private void Explode()
    {
        Debug.Log("exploding");
        source.Play();
        range = 1.2f;
        animator.SetTrigger("EndLife");
        exploded = true;

        Collider2D[] hits = Physics2D.OverlapCircleAll(explosionPoint.position, range, enemyLayers);

        foreach (Collider2D obj in hits)
        {
            EnemyHealthSystem enemy = obj.gameObject.GetComponent<EnemyHealthSystem>();

            enemy.TakeDamage(explosionDamage);
        }

        Destroy(gameObject, 3f);
    }

    private IEnumerator LifeTime(int type)
    {
        if (type == 1)
        {
            yield return new WaitForSeconds(1.6f);
            if(!exploded)
            {
                rb.drag = 1.2f;
                Debug.Log("end of energy");
            }
            yield return new WaitForSeconds(0.9f);
            
            Explode();
            
        }
        else if (type == 2)
        {
            yield return new WaitForSeconds(0.25f);
            if (!exploded)
            {
                Explode();
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!exploded && collision.transform.tag != "Player" && collision.transform.tag != "Clone" && collision.transform.tag != "Confiner")
        {
            rb.drag = 1.75f;
            Debug.Log("collided");
            Debug.Log(collision);
            StartCoroutine(LifeTime(2));
        }

        Collider2D[] hits = Physics2D.OverlapCircleAll(firePoint.position, range, enemyLayers);

        foreach (Collider2D obj in hits)
        {
            EnemyHealthSystem enemy = obj.gameObject.GetComponent<EnemyHealthSystem>();

            enemy.TakeDamage(fireDamage);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (firePoint == null || explosionPoint == null)
            return;

        Gizmos.DrawWireSphere(firePoint.position, 0.3f);
        Gizmos.DrawWireSphere(explosionPoint.position, 1.2f);
    }

}
