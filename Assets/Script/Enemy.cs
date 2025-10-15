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
    public int facingDirection = 1;

    [Header("Raycast Settings")]
    public float detectDistance = 1.5f;
    public LayerMask obstacleMask;
    private Vector2 avoidDirection = Vector2.zero;
    private float avoidTime = 0f;
    public float zombieSpeed = 2f;
    private bool isDead = false;


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
        if (isDead) return;
        healthSystem.TakeDamage(dmg);
        enemyAnimator.PlayHit();

        if (healthSystem.currentHealth <= 0)
        {
            isDead = true;
            enemyAnimator.PlayDie();
            //StartCoroutine(DelayedDestroy(5f));
            StartCoroutine(DestroyAfterAnimation());
            
        }
    }
    //private IEnumerator DelayedDestroy(float delay)
    //{
    //    yield return new WaitForSeconds(delay);
    //    Destroy(gameObject);
    //}
    private IEnumerator DestroyAfterAnimation()
    {
        Animator anim = GetComponent<Animator>();
        yield return new WaitUntil(() => anim.GetCurrentAnimatorStateInfo(0).IsName("isDead"));
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);
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
        if (target.position.x < transform.position.x && transform.localScale.x > 0) Flip();
        else if (target.position.x > transform.position.x && transform.localScale.x < 0) Flip();

    }

    private void MoveTowardsTarget()
    {
        Vector2 directionToPlayer = (target.position - transform.position).normalized;

        // Nếu đang né
        if (avoidTime > 0)
        {
            transform.position += (Vector3)(avoidDirection * zombieSpeed * Time.deltaTime);
            avoidTime -= Time.deltaTime;
            return;
        }

        // Bắn ray kiểm tra vật cản
        RaycastHit2D hit = Physics2D.Raycast(transform.position, directionToPlayer, detectDistance, obstacleMask);
        Debug.DrawRay(transform.position, directionToPlayer * detectDistance, Color.red);

        if (hit.collider != null)
        {
            float randomAngle = Random.Range(-90f, 90f);
            avoidDirection = Quaternion.Euler(0, 0, randomAngle) * directionToPlayer;
            avoidTime = 0.5f;
            Debug.Log($"{gameObject.name} gặp vật cản, né sang hướng khác!");
        }
        else
        {
            transform.position += (Vector3)(directionToPlayer * zombieSpeed * Time.deltaTime);
        }
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
    void Flip()
    {
        facingDirection *= -1;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }
    
}