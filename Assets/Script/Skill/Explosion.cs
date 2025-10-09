using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public float radius = 4f;
    public int damage = 100;

    public void Setup(float r, int dmg)
    {
        radius = r;
        damage = dmg;
        Explode();
    }

    void Explode()
    {
        Collider2D[] targets = Physics2D.OverlapCircleAll(transform.position, radius);
        foreach (var target in targets)
        {
            if (target.CompareTag("Enemy"))
            {
                target.GetComponent<Enemy>().TakeDamage(damage);
            }
        }
        Destroy(gameObject);
    }

}
