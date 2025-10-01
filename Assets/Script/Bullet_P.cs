using UnityEngine;

public class Bullet_P : MonoBehaviour
{
    public float speed = 10f;         // Tốc độ bay
    public float damage = 50f;        // Sát thương
    public float lifetime = 2f;       // Thời gian tồn tại

    private Rigidbody2D rb;

    public void Initialize(Vector2 direction)
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = direction * speed; // speed là tốc độ cố định, ví dụ: 10f
        Destroy(gameObject, lifetime);
        Debug.Log("Vận tốc đạn: " + rb.velocity.magnitude);
    }

    void OnTriggerEnter2D(Collider2D other)
    {

        Debug.Log("Va chạm với: " + other.name);

        // Kiểm tra xem có phải là Enemy không
        Enemy enemy = other.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage); // Gây sát thương
            OnHitEnemy(enemy); // Gọi hàm kiểm tra trúng
            
        }
        Destroy(gameObject);
        // Hủy đạn sau va chạm
    }
    void OnHitEnemy(Enemy enemy)
    {
        Debug.Log("Đã bắn trúng: " + enemy.name);

    }
}