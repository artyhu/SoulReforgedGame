using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Aiming : MonoBehaviour
{
    [SerializeField] private Camera cam;
    public Vector3 mousePos { get; set; }
    private Rigidbody2D rb;
    private SpriteRenderer sprite;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    private void OnLook(InputValue posValue)
    {
        Vector2 posVector = cam.ScreenToWorldPoint(posValue.Get<Vector2>());
        float faceDir = posVector.x - rb.position.x;

        if (faceDir < 0f)
        {

            /*
            Vector3 scale = transform.localScale;
            scale.x *= -1f;
            transform.localScale = scale;
            */
            sprite.flipX = true;
        }
        else
        {
            sprite.flipX = false;
        }

        mousePos = new Vector2(posVector.x, posVector.y);
    }
}
