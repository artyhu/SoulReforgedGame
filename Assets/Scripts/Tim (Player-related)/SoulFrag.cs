using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulFrag : MonoBehaviour
{
    [SerializeField]
    private Buffs heal;

    [SerializeField]
    private Buffs souls;

    public float reduction { get; set; }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
            heal.Apply(collision.gameObject, reduction);
            souls.Apply(collision.gameObject);
        }
    }
}
