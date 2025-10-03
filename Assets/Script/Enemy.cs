using UnityEngine;

public class Enemy : MonoBehaviour
{
    // //public float health = 100f;
    // public float damage = 10f;

    // public virtual void TakeDamage(float damage)
    // {
    //     health -= damage;
    //     if (health <= 0)
    //     {
    //         Die();
    //     }
    // }

    // private void OnCollisionEnter2D(Collision2D collision)
    // {
    //     if (collision.gameObject.CompareTag("player"))
    //     {
    //         PlayerHealth player = collision.gameObject.GetComponent<PlayerHealth>();
    //         if (player != null)
    //         {
    //             player.TakeDamage(damage);
    //         }
    //     }
    // }
    // protected virtual void Die()
    // {
    //     Destroy(gameObject);
    // }

    public float damage = 10f;

    private Health healthSystem;

    void Awake()
    {
        // Lấy HealthSystem gắn trên Enemy
        healthSystem = GetComponent<Health>();
    }

    public void TakeDamage(float damage)
    {
        if (healthSystem != null)
        {
            healthSystem.TakeDamage(damage);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("player"))
        {
            PlayerHealth player = collision.gameObject.GetComponent<PlayerHealth>();
            if (player != null)
            {
                player.TakeDamage(damage);
            }
        }
    }

}