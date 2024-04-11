using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    [SerializeField]
    private Transform[] moveSpots;

    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer sprite;

    private MageCombat mage;

    private Transform player, target, moveSpot;

    private Vector2 startPosition;

    [SerializeField]
    private AllowExit exitControl;

    [SerializeField]
    private float speed, detectionDistance, stoppingDistance, retreatDistance, resetDistance;

    private string state;

    private bool canAttack = true, waiting, resetting;
    private short retreat = 1;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        mage = GetComponent<MageCombat>();
        
    }
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        startPosition = moveSpots[0].position;
        state = "patrol";
        newPos();
    }

    void FixedUpdate()
    {

        // try and get clone's transform
        Transform clone = null;
        GameObject c = GameObject.FindWithTag("Clone");
        if (c)
        {
            clone = c.transform;
        }

        // target closer between player and clone
        if (clone && Vector2.Distance(rb.position, clone.position) < Vector2.Distance(rb.position, player.position))
        {
            target = clone;
        }
        else
        {
            target = player;
        }

        // in hostile distance, too close
        float toTarget = Vector2.Distance(rb.position, target.position);
        if (!resetting && toTarget < detectionDistance)
        {
            exitControl.Deactive();
            state = "hostile";
        }

        switch (state)
        {
            case "patrol":
                // starts at patrol
                animator.SetBool("Moving", true);

                Vector3 scale = transform.localScale;
                if (moveSpot.position.x < transform.position.x)
                {
                    scale.x = Mathf.Abs(scale.x) * -1;
                }
                else
                {
                    scale.x = Mathf.Abs(scale.x);
                }
                transform.localScale = scale;

                rb.position = Vector2.MoveTowards(rb.position, moveSpot.position, speed * Time.deltaTime);

                // stops for few seconds and fetch new random moveSpot location
                if (Vector2.Distance(rb.position, moveSpot.position) < 0.2f)
                {
                    if (resetting)
                    {
                        resetting = false;
                    }

                    float waitTime = Random.Range(4f, 10f);
                    StartCoroutine(Wait(0, waitTime));
                }
                break;

            case "waiting":
                mage.invulnerable = false;
                animator.SetBool("Moving", false);
                break;

            case "hostile":

                if (mage.invulnerable)
                {
                    mage.invulnerable = false;
                }

                // move closer to target
                if (toTarget > stoppingDistance)
                {
                    retreat = 1;
                    rb.position = Vector2.MoveTowards(rb.position, target.position, speed * Time.deltaTime);
                    animator.SetBool("Moving", true);
                }
                /* at kite back distance
                else if (toTarget < retreatDistance)
                {
                    retreat = -1;
                    rb.position = Vector2.MoveTowards(rb.position, target.position, -speed * Time.deltaTime);
                    animator.SetBool("Moving", true);
                }
                */
                // too far out
                else if (Vector2.Distance(rb.position, startPosition) > resetDistance)
                {
                    state = "patrol";
                    mage.invulnerable = true;
                    mage.resetHp();
                    resetting = true;
                    
                }
                // attacking
                else
                {
                    retreat = 1;
                    animator.SetBool("Moving", false);
                    if (canAttack)
                    {
                        mage.Attack(Random.Range(0, 3), target);
                        canAttack = false;
                        StartCoroutine(Wait(1, 5f));
                    }
                }

                if (!waiting)
                {
                    StartCoroutine(Wait(2, 0.3f));
                    waiting = true;
                }

                break;
        }

        // flip x if moving left
        /*
        if (rb.position.x - oldPosition.x < 0)
        {
            sprite.flipX = true;
        }
        else
        {
            sprite.flipX = false;
        }
        */

    }

    private void newPos()
    {
        int randomSpot = Random.Range(0, moveSpots.Length-1);
        moveSpot = moveSpots[randomSpot];
    }

    private IEnumerator Wait(int type, float waitTime)
    {
        if (type == 0)
        {
            state = "waiting";
            yield return new WaitForSeconds(waitTime);
            // patrol only if it's not in hostile state
            if (state != "hostile")
            {
                newPos();
                state = "patrol";
            }
        }

        else if (type == 1)
        {
            yield return new WaitForSeconds(waitTime);
            Debug.Log("hand can attack");
            canAttack = true;
        }

        else if (type == 2)
        {
            yield return new WaitForSeconds(waitTime);
            Vector3 targetScale = transform.localScale;
            if (target.position.x < transform.position.x)
            {
                targetScale.x = Mathf.Abs(targetScale.x) * -1 * retreat;
            }
            else
            {
                targetScale.x = Mathf.Abs(targetScale.x) * retreat;
            }
            transform.localScale = targetScale;

            waiting = false;
        }


    }

    
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, detectionDistance);
        Gizmos.DrawWireSphere(transform.position, stoppingDistance);
        Gizmos.DrawWireSphere(transform.position, retreatDistance);
        Gizmos.DrawWireSphere(transform.position, resetDistance);
    }
    
}
