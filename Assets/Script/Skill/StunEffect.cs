using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunEffect : MonoBehaviour
{
    public float range = 10f;
    public float duration = 3f;

    public void Setup(float r, float d)
    {
        range = r;
        duration = d;
        ApplyStun();
    }

    void ApplyStun()
    {
        Collider2D[] targets = Physics2D.OverlapCircleAll(transform.position, range);
        foreach (var target in targets)
        {
            if (target.CompareTag("Enemy"))
            {
                target.GetComponent<Enemy>().ApplyStun(duration);
            }
        }
        Destroy(gameObject);
    }

}
