using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;

    Rigidbody2D rigidbody2d;

    public int damage;

    public List<Enemy> enemies = new List<Enemy>();

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        rigidbody2d.AddRelativeForce(new Vector2(1, 0) * speed);

        Destroy(gameObject, 3);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("BulletDeath"))
        {
            Destroy(gameObject,0.1f);
        }

        if (collision.CompareTag("Enemy"))
        {            
            enemies.Add(collision.GetComponent<Enemy>());
            enemies[0].TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
