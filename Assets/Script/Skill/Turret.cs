using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public float range = 5f;
    public int damage = 20;
    public float fireRate = 1f;

    private float fireCooldown;

    public void Setup(float r, int dmg)
    {
        range = r;
        damage = dmg;
    }

    void Update()
    {
        fireCooldown -= Time.deltaTime;
        if (fireCooldown <= 0f)
        {
            Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, range);
            foreach (var enemy in enemies)
            {
                if (enemy.CompareTag("Enemy"))
                {
                    enemy.GetComponent<Enemy>().TakeDamage(damage);
                    fireCooldown = fireRate;
                    break;
                }
            }
        }
    }

}
