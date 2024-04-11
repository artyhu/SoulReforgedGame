using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 9.0f;
    private Vector2 movement;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        movement = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;
        animator.SetFloat("Speed", Mathf.Abs(movement.magnitude * movementSpeed));

        bool flipped = movement.x < 0;
        this.transform.rotation = Quaternion.Euler(new Vector3(0f, flipped ? 180f : 0f, 0f));

        if (Input.GetKeyDown("space")) {
            animator.SetBool("sl", true);
        }

        if (Input.GetKeyDown("e")) {
            animator.SetBool("da", true);
            bool move = movement.x == 0 && movement.y == 0;
            this.transform.position = new Vector2(transform.position.x + (move ? 3 : movement.x)*3 ,transform.position.y + (movement.y)*3); 
            
        }

    }

    private void FixedUpdate() {
        if (movement != Vector2.zero) {
            var xMovement = movement.x * movementSpeed * Time.deltaTime;
            var yMovement = movement.y * movementSpeed * Time.deltaTime;
            this.transform.Translate(new Vector3(xMovement, yMovement), Space.World);
        }

    }
}
