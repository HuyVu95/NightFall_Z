using UnityEngine;

public class Bullet_P : MonoBehaviour
{
    public float speed = 10f;         
    public float damage = 50f;        
    public float lifetime = 2f;       

    private Rigidbody2D rb;

    public void Initialize(Vector2 direction)
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = direction * speed; 
        Destroy(gameObject, lifetime);
        Debug.Log("Vận tốc đạn: " + rb.velocity.magnitude);
    }

    void OnTriggerEnter2D(Collider2D other)
    {

        Debug.Log("Va chạm với: " + other.name);

        
        Enemy enemy = other.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage); 
            OnHitEnemy(enemy); 
            
        }
        Destroy(gameObject);
        
    }
    void OnHitEnemy(Enemy enemy)
    {
        Debug.Log("Đã bắn trúng: " + enemy.name);

    }
}