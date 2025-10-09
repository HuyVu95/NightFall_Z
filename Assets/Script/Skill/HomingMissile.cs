using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingMissile : MonoBehaviour
{
    public float speed = 5f;
    public int damage = 100;
    private Transform target;

    public void Setup(int dmg)
    {
        damage = dmg;
        target = FindClosestEnemy();
    }

    void Update()
    {
        if (target == null) return;
        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        if (Vector2.Distance(transform.position, target.position) < 0.2f)
        {
            target.GetComponent<Enemy>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }

    Transform FindClosestEnemy()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, 10f);
        foreach (var e in enemies)
        {
            if (e.CompareTag("Enemy")) return e.transform;
        }
        return null;
    }

}
