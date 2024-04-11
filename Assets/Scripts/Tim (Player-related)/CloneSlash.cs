using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneSlash : MonoBehaviour
{

    private Aiming player;
    [SerializeField] private PlayerHealth health;
    [SerializeField] private float drainAmount = 2f;

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
    }
    public void Attack(GameObject fragment)
    {
        fragment.GetComponent<SoulFrag>().reduction = drainAmount;
        health.Heal(drainAmount);
    }
}
