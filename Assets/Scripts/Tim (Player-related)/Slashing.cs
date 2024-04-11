using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slashing : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer characterRenderer, weaponRenderer;
    [SerializeField] private Animator animator;
    private Aiming player;

    private void Awake()
    {
        player = GetComponentInParent<Aiming>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 direction = (player.mousePos - transform.position).normalized;
        transform.right = direction;
        Vector2 scale = transform.localScale;

        
        if (direction.x < 0)
        {
            scale.y = -1;
        }
        else if (direction.x > 0)
        {
            scale.y = 1;
        }

        transform.localScale = scale;
        

        if (transform.eulerAngles.z > 0 && transform.eulerAngles.z < 180)
        {
            weaponRenderer.sortingOrder = characterRenderer.sortingOrder - 1;
        }
        else
        {
            weaponRenderer.sortingOrder = characterRenderer.sortingOrder + 1;
        }
    }

    public void Attack()
    {
        animator.SetTrigger("Slash");
    }
}
