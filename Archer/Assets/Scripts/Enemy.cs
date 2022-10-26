using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [Header("ATR")]
    public float healt;
    [SerializeField]
    protected float _healt;
    public float speed;
    protected float attackSpeed = 1f;
    public int damage;

    [Space]
    public int gold;
    public int exp;
    public int level;

    [Space]
    public float spawnOffset;


    protected Rigidbody2D rigidbody2d;
    protected Animator animator;

    public SkeletonAnimation skeletonAnim;
    public MeshRenderer meshRenderer;
    Attribiutes atr;

    private void Awake()
    {
        atr = GameObject.FindObjectOfType<Attribiutes>();
        meshRenderer = GetComponent<MeshRenderer>();

    }

    protected virtual void Start()
    {
        skeletonAnim = GetComponent<SkeletonAnimation>();
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        _healt = healt;
        transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RectTransform>().localScale = new Vector3(_healt / healt, 1, 1);


        Move();
    }



    public void TakeDamage(int damage)
    {
        _healt -= damage;

        if (_healt > 0)
        {
            transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RectTransform>().localScale = new Vector3(_healt / healt, 1, 1);
        }

        if (_healt <= 0)
        {
            rigidbody2d.velocity = Vector2.zero;
            Death();
        }
    }

    void Death()
    {
        rigidbody2d.velocity = Vector2.zero;
        GetComponent<BoxCollider2D>().enabled = false;
        skeletonAnim.AnimationName = "Dead";
        //animator.SetTrigger("DeathT");
        atr.GetMoney(gold);
        atr.GetExp(exp);
        Destroy(gameObject);
    }

    public void Move()
    {
        rigidbody2d.AddForce(Vector2.left * speed);
    }
}
