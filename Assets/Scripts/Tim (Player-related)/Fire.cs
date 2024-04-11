using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    private Aiming player;
    public Vector2 fireDir;

    private void Awake()
    {
        player = GetComponentInParent<Aiming>();
    }

    // Update is called once per frame
    void Update()
    {
        fireDir = (player.mousePos - transform.position).normalized;
        float angle = Mathf.Atan2(fireDir.y, fireDir.x) * Mathf.Rad2Deg;

        transform.eulerAngles = new Vector3(0, 0, angle);
    }
}
