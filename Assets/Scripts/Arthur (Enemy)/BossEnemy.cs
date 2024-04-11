using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy : MonoBehaviour
{
    private Transform player;
    public Transform firingPoint;
    public Transform gun;
    
    public GameObject enemyProjectile;
    [SerializeField] float moveSpeed = 3f;
    public float followPlayerRange;
    private bool inRange;
    public float attackRange;
 
    public float firingRate;
    private float firing;
 
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    void Update()
    {
        Vector3 differance = player.position - gun.transform.position;
        float rotate = Mathf.Atan2(differance.y, differance.x) * Mathf.Rad2Deg;
        gun.transform.rotation = Quaternion.Euler(0f, 0f, rotate);
 
        if (Vector2.Distance(transform.position, player.position) <= followPlayerRange && Vector2.Distance(transform.position, player.position) > attackRange)
        {
            inRange = true;
        }
        else
        {
            inRange = false;
        }
 
        if (Vector2.Distance(transform.position, player.position) <= attackRange)
        {
            if (firing <= 0)
            {
                Instantiate(enemyProjectile, firingPoint.position, firingPoint.transform.rotation);
                firing = firingRate;
            }
            else
            {
                firing -= Time.deltaTime;
            }
        }
    }
 
    void FixedUpdate()
    {
        if (inRange)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
        }
    }

    void OnDrawGizmos() {
        Gizmos.DrawWireSphere(transform.position, followPlayerRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

}
 
