using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float damage = 10f;

    private Health healthSystem;
    public static int AliveCount;
    private bool isStunned = false;
    private float stunTimer = 0f;

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
        AliveCount = Mathf.Max(AliveCount - 1, 0);

        Debug.Log($"Zombie bị diệt. Hiện tại còn {AliveCount} zombie sống.");

        // Nếu hết zombie thì báo cho GameManager
        if (AliveCount <= 0)
        {
            GameManager.Instance.CheckWaveClear();
        }
    }
    public void ApplyStun(float duration)
    {
        isStunned = true;
        stunTimer = duration;
        Debug.Log($"{gameObject.name} bị choáng trong {duration} giây");
    }
    void Update()
    {
        if (isStunned)
        {
            stunTimer -= Time.deltaTime;
            if (stunTimer <= 0f)
            {
                isStunned = false;
                Debug.Log($"{gameObject.name} hết choáng");
            }

            // Nếu có hệ thống di chuyển hoặc tấn công, bạn có thể chặn tại đây:
            // return; hoặc skip hành vi
        }

        if (!isStunned)
        {
            // Enemy hành động bình thường (di chuyển, tấn công…)
        }
    }



}