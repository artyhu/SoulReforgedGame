using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private bool canDash = true;
    private bool dashing = false;

    // moving and looking
    public float movementX;
    public float movementY;
    private float cdTime;
    private float activeTime;
    [SerializeField] private float speed = 4f;

    private Rigidbody2D rb;
    public Ability ability;
    private Animator animator;
    private PlayerSounds sounds;


    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sounds = GetComponent<PlayerSounds>();
    }

    void FixedUpdate()
    {
        if (!PauseMenuController.isPaused)
        {

        Vector2 movement = new Vector2(movementX, movementY);
        rb.velocity = movement * speed;
        animator.SetFloat("Speed", (movement * speed).sqrMagnitude);
        }
        /*
        Vector2 facingDir = mousePos - rb.position;
        float angle = Mathf.Atan2(facingDir.y, facingDir.x) * Mathf.Rad2Deg;
        rb.MoveRotation(angle);
        */
    }

    private void OnMove(InputValue movementValue)
    {
                if (!PauseMenuController.isPaused)
        {

        if (!dashing)
        {
            Vector2 movementVector = movementValue.Get<Vector2>();

            movementX = movementVector.x;
            movementY = movementVector.y;

        }
        }

    }

    private void OnDash()
    {
                if (!PauseMenuController.isPaused)
        {

        if (canDash)
        {
            sounds.PlayAudio("d");
            ability.Activate(gameObject);
            activeTime = ability.activeTime;
            speed = 10f;
            canDash = false;
            dashing = true;
            
            StartCoroutine(Dashing());
        }
        }
    }
    private IEnumerator Dashing()
    {
        yield return new WaitForSeconds(activeTime);
        dashing = false;
        speed = 4f;
        cdTime = ability.cd;
        if(!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.D))
        {
            movementX = 0;
            movementY = 0;
        }

        yield return new WaitForSeconds(cdTime);
        canDash = true;
    }
}
