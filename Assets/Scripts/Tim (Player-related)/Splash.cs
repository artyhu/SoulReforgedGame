using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Splash : MonoBehaviour
{

    private Aiming player;
    public Vector3 ZapDir;
    [SerializeField] private Transform stable;

    private void Awake()
    {
        player = GetComponentInParent<Aiming>();
    }

    // Update is called once per frame
    void Update()
    {
        ZapDir = player.mousePos - transform.position;
        
        float angle = Mathf.Atan2(ZapDir.y, ZapDir.x) * Mathf.Rad2Deg;

        transform.eulerAngles = new Vector3(0, 0, angle);

        /*
        Vector2 scale = transform.localScale;

        
        if (ZapDir.y < 0)
        {
            scale.y = -1;
        }
        else if (ZapDir.y > 0)
        {
            scale.y = 1;
        }
        
        if (ZapDir.x < 0)
        {
            scale.x = 1;
        }
        else if (ZapDir.x > 0)
        {
            scale.x = -1;
        }
        
        transform.localScale = scale;
        */
    }
}
