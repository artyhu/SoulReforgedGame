using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterTest : MonoBehaviour
{
    // moving and looking
    public float movementX;
    public float movementY;

    private bool canDash = true;
    private bool dashing = false;
    private float cdTime;
    private float activeTime;
    public Ability ability;

    public Vector2 mousePos;

    private Rigidbody2D rb;
    [SerializeField] private float speed = 3f;
    [SerializeField] Camera cam;
    [SerializeField] private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        Vector2 movement = new Vector2(movementX, movementY);
        rb.velocity = movement * speed;

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);

        animator.SetFloat("Speed", movement.sqrMagnitude);
    }

    private void OnMove(InputValue movementValue)
    {
        if (!dashing)
        {
            Vector2 movementVector = movementValue.Get<Vector2>();

            movementX = movementVector.x;
            movementY = movementVector.y;
        }

    }

    private void OnDash()
    {
        if (canDash)
        {
            animator.SetTrigger("Jump");
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
        speed = 3f;
        cdTime = ability.cd;
        dashing = false;

        yield return new WaitForSeconds(cdTime);
        canDash = true;
    }
}
