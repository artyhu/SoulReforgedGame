using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clone : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField]
    private GameObject slashEffect;
    private Animator animator;

    // Start is called before the first frame update

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        Inactive();

    }

    // Update is called once per frame
    void FixedUpdate()
    {

    }

    public void Inactive()
    {
        // reset movement and velocity
        // movement.movementX = 0;
        // movement.movementY = 0;
        rb.velocity = new Vector2(0, 0);

        // reset rotation
        gameObject.transform.rotation = Quaternion.identity;

        // disable rigidbody
        rb.Sleep();
        slashEffect.SetActive(false);
        gameObject.SetActive(false);
    }

    public void Active(Transform spawnPoint)
    {

        rb.WakeUp();
        gameObject.transform.position = spawnPoint.position;
        gameObject.SetActive(true);

        Debug.Log("Clone zap status check");
        PlayerCombat player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCombat>();
        PlayerCombat clone = gameObject.GetComponent<PlayerCombat>();

        if (player.zapStatus)
        {
            clone.zapStatus = false;
        }
        else
        {
            clone.zapStatus = true;
        }

        clone.resetCD();

    }

    public void cloneHit()
    {
        animator.SetTrigger("Hurt");
        // stop movement or anything else
    }
}
