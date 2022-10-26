using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWarrior : Enemy
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Brain"))
        {
            rigidbody2d.velocity = Vector2.zero;
            StartCoroutine(Attack(GameObject.FindObjectOfType<PlayerController>()));
        }
    }

    IEnumerator Attack(PlayerController controller)
    {
        while (true)
        {
            skeletonAnim.AnimationName = "Attack";
            //animator.SetBool("Attack", true);
            controller.TakeDamage(damage);
            //attackSpeed = animator.GetCurrentAnimatorClipInfo(0).Length;
            attackSpeed = 1f;
            yield return new WaitForSeconds(attackSpeed);
        }
    }
}
