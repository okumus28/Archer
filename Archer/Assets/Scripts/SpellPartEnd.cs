using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellPartEnd : MonoBehaviour
{
    public int damage;

    List<Enemy> enemies = new List<Enemy>();

    private void Start()
    {
        Destroy(gameObject , 0.3f);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<Enemy>().TakeDamage(damage);
        }
    }
}
