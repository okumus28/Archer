using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Spell : MonoBehaviour
{
    public SpellPart spellPart;
    public SpellPartEnd spellPartEnd;

    public int partCount;

    CircleCollider2D circleCollider2d;

    public bool canDamage = false;

    public int damage;

    public bool dragging = false;

    public Vector3 offset;

    public Vector3 spellPartEndOffset;

    public int angle;

    private void Awake()
    {
        circleCollider2d = GetComponent<CircleCollider2D>();
    }
    private void Update()
    {
        if (dragging)
        {
            Vector3 position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, +10);
            transform.position = Camera.main.ScreenToWorldPoint(position + new Vector3(offset.x, offset.y));
        }
    }

    public void PartGo()
    {
        for (int i = 0; i < partCount; i++)
        {
            Bounds bounds = this.circleCollider2d.bounds;

            float x = Random.Range(bounds.min.x, bounds.max.x);
            float y = Random.Range(bounds.min.y, bounds.max.y);

            Vector3 startPosition = new Vector3(transform.position.x, transform.position.y + 12);

            SpellPart sP = Instantiate(spellPart, startPosition, Quaternion.Euler(0, 0, angle));
            sP.target = new Vector3(x, y, 0);
            sP.spellPartEnd = this.spellPartEnd;
            sP.damage = this.damage;
        }
    }
}
