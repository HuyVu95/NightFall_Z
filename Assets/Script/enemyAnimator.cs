using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
        private Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // Đi bộ
    public void SetWalking(bool isWalking)
    {
        animator.SetBool("isWalking", isWalking);
    }

    // Tấn công
    public void SetAttacking(bool isAttacking)
    {
        animator.SetBool("isAttacking", isAttacking);
    }

    // Bị trúng đòn
    public void PlayHit()
    {
        animator.SetTrigger("Hit");
    }

    // Chết
    public void PlayDie()
    {
        animator.SetTrigger("Die");
    }
}
