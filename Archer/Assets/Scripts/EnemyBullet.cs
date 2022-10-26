using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float speed;

    Rigidbody2D rigidbody2d;
    Animator animator;

    public int damage;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        rigidbody2d.AddRelativeForce(new Vector2(1, 0) * -speed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Brain"))
        {
            rigidbody2d.velocity = Vector2.zero;
            GameObject.FindObjectOfType<PlayerController>().TakeDamage(damage);
            animator.SetBool("FireEnd" , true);
            Destroy(gameObject , animator.GetCurrentAnimatorClipInfo(0).Length);
        }
    }
}
