using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestState : MonoBehaviour
{



    private Animator animator;
    private float timer;
    private int stage;

    void Start()
    {
        animator = GetComponent<Animator>();
        timer = 0f;
        stage = 0;
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= 2f)
        {
            timer = 0f;
            stage++;

            switch (stage)
            {
                case 1:
                    Debug.Log("Stage 1: Walk");
                    animator.SetBool("isWalking", true);
                    break;

                case 2:
                    Debug.Log("Stage 2: Hit");
                    animator.SetBool("isWalking", false);
                    animator.SetTrigger("Hit");
                    break;

                case 3:
                    Debug.Log("Stage 3: Walk again");
                    animator.SetBool("isWalking", true);
                    break;

                case 4:
                    Debug.Log("Stage 4: Attack");
                    animator.SetBool("isWalking", false);
                    animator.SetBool("isAttacking", true);
                    break;

                case 5:
                    Debug.Log("Stage 5: Die");
                    animator.SetBool("isAttacking", false);
                    animator.SetTrigger("Die");
                    break;
            }
        }
    }
}
