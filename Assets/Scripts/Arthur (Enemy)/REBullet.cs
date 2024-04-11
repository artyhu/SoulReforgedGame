using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class REBullet : MonoBehaviour
{

    public float bulletSpeed;
    public float lifeTime;
    [SerializeField] private float bulletDamage = 3f;

    void Start()
    {
        Invoke("DestroyProjectile", lifeTime);
    }
 
    void FixedUpdate()
    {
        transform.Translate(Vector2.right * bulletSpeed * Time.deltaTime);
    }
 
    private void DestroyProjectile()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerHealth>().TakeDamage(bulletDamage, true);
        }

        else if (collision.gameObject.tag == "Clone")
        {
            collision.gameObject.GetComponent<PlayerHealth>().TakeDamage(bulletDamage);
            collision.gameObject.GetComponent<Clone>().cloneHit();
        }
    }


}