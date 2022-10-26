using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellPart : MonoBehaviour
{

    public Vector3 target;
    public float speed = 10f;
    public int damage;
    public Vector3 offset;

    public SpellPartEnd spellPartEnd;

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position , target + offset , Random.Range(speed -10 , speed +10) * Time.deltaTime);
        if (transform.position == target + offset)
        {
            if (spellPartEnd != null)
            {
                SpellPartEnd sPE = Instantiate(spellPartEnd, transform.position, Quaternion.identity);
                sPE.damage = this.damage;
            }            
            Destroy(gameObject , 0.1f);
        }
    }
}
