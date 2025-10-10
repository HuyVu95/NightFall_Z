using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(EnemyAnimator))]
public class Enemy : MonoBehaviour
{
    [Header("Combat Settings")]
    public float damage = 10f;
    public float attackRange = 1.5f;
    public float attackCooldown = 1.5f;

    [Header("Status")]
    public static int AliveCount;
    private bool isStunned = false;
    private float stunTimer = 0f;
    private float attackTimer = 0f;

    private Health healthSystem;
    private EnemyAnimator enemyAnimator;
    private Transform target;

    void Awake()
    {
        healthSystem = GetComponent<Health>();
        enemyAnimator = GetComponent<EnemyAnimator>();
        target = GameObject.FindGameObjectWithTag("player")?.transform;
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

        if (AliveCount <= 0)
        {
            GameManager.Instance.CheckWaveClear();
        }
    }

    public void TakeDamage(float dmg)
    {
        healthSystem.TakeDamage(dmg);
        enemyAnimator.PlayHit();

        if (healthSystem.currentHealth <= 0)
        {
            enemyAnimator.PlayDie();
            StartCoroutine(DelayedDestroy(5f));
        }
    }
    private IEnumerator DelayedDestroy(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
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
            return;
        }

        if (target == null) return;

        float distance = Vector2.Distance(transform.position, target.position);

        if (distance > attackRange)
        {
            enemyAnimator.SetWalking(true);
            enemyAnimator.SetAttacking(false);
            MoveTowardsTarget();
        }
        else
        {
            enemyAnimator.SetWalking(false);
            HandleAttack();
        }
    }

    private void MoveTowardsTarget()
    {
        Vector2 direction = (target.position - transform.position).normalized;
        transform.position += (Vector3)(direction * Time.deltaTime);
    }

    private void HandleAttack()
    {
        attackTimer -= Time.deltaTime;

        if (attackTimer <= 0f)
        {
            enemyAnimator.SetAttacking(true);
            PlayerHealth player = target.GetComponent<PlayerHealth>();
            if (player != null)
            {
                player.TakeDamage(damage);
            }
            attackTimer = attackCooldown;
        }
        else
        {
            enemyAnimator.SetAttacking(false);
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