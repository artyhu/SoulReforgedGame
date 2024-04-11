using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    public Ability ability;
    private Animator animator;
    [SerializeField]
    private Splitter splitter;
    private float speed = 4f;
    private float cdTime;
    private float activeTime;
    private bool canDash = true;
    private bool dashing = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        //Vector2 towards = new Vector2(splitter.spawnPoint.position.x, splitter.spawnPoint.position.y);
        //rb.velocity = (rb.position - towards) * speed;
        rb.position = Vector2.MoveTowards(transform.position, splitter.spawnPoint.position, speed * Time.fixedDeltaTime);
        animator.SetFloat("Speed", ((transform.position - splitter.spawnPoint.position) * speed).sqrMagnitude);
    }
    private void OnDash()
    {
        if (canDash)
        {
            ability.Activate(gameObject);
            activeTime = ability.activeTime;
            speed = 10f;
            canDash = false;
            dashing = true;

            StartCoroutine(Dashing());
        }
    }
    private IEnumerator Dashing()
    {
        yield return new WaitForSeconds(activeTime);
        speed = 4f;
        cdTime = ability.cd;
        dashing = false;

        yield return new WaitForSeconds(cdTime);
        canDash = true;
    }

}
