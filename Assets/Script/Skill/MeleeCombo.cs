using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeCombo : MonoBehaviour
{
    public int hits = 3;
    public int damage = 100;

    public void Setup(int h, int dmg)
    {
        hits = h;
        damage = dmg;
        StartCoroutine(ComboAttack());
    }

    System.Collections.IEnumerator ComboAttack()
    {
        for (int i = 0; i < hits; i++)
        {
            Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, 2f);
            foreach (var enemy in enemies)
            {
                if (enemy.CompareTag("Enemy"))
                {
                    enemy.GetComponent<Enemy>().TakeDamage(damage);
                }
            }
            yield return new WaitForSeconds(0.3f);
        }
        Destroy(gameObject);
    }

}
