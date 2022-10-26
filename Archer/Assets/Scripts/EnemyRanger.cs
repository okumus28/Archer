using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRanger : Enemy
{

    [Header("ENEMY RANGER")]
    public EnemyBullet bullet;
    Transform firePoint;

    public float _attackSpeed;

    bool startAttackCoroutine = false;

    public float scale;

    protected override void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        _healt = healt;

        rigidbody2d.AddForce(Vector2.left * speed);

        firePoint = transform.GetChild(0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Range"))
        {
            rigidbody2d.velocity = Vector2.zero;
            if (!startAttackCoroutine)
            {
                StartCoroutine(Attack(GameObject.FindObjectOfType<PlayerController>()));
            }
        }
    }

    IEnumerator Attack(PlayerController controller)
    {
        startAttackCoroutine = true;
        while (true)
        {
            animator.SetBool("Attack", true);
            yield return new WaitForSeconds(0.01f);
            animator.SetBool("Attack", false);
            animator.SetTrigger("IdleT");
            //controller.TakeDamage(damage);
            attackSpeed = animator.GetCurrentAnimatorClipInfo(0).Length;
            yield return new WaitForSeconds(_attackSpeed);
        }
    }

    public void EnemyBulletEvent()
    {
        EnemyBullet _bullet = Instantiate(bullet, firePoint.position, Quaternion.identity);
        _bullet.transform.localScale *= scale;
        _bullet.damage = this.damage;
    }
}
