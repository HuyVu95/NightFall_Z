using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float damage = 10f;

    private Health healthSystem;
    public static int AliveCount = 0;
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
    void OnEnable()
    {
        AliveCount++;
        Debug.Log($"Zombie xuất hiện. Hiện tại còn {AliveCount} zombie sống.");
    }

    void OnDestroy()
    {
        AliveCount--;
        Debug.Log($"Zombie bị diệt. Hiện tại còn {AliveCount} zombie sống.");

        // Nếu hết zombie thì báo cho GameManager
        if (AliveCount <= 0)
        {
            GameManager.Instance.CheckWaveClear();
        }
    }

}