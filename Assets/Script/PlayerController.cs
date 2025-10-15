using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed = 3f;
    private Vector2 moveInput;
    public int facingDirection = 1;
    public Animator anim;
    public bool isDead = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isDead) return;
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");
        moveInput.Normalize();
        
        FlipTowardsMouse();
        if (anim != null)
        {
            
            bool isMoving = moveInput.magnitude > 0; // kiểm tra có đang di chuyển không
            bool isIdleNow = moveInput.magnitude == 0;
            anim.SetBool("isWalking", isMoving);
            anim.SetBool("isIdle", isIdleNow);
        }

    }
    void FlipTowardsMouse()
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 playerPos = transform.position;
        if (mouseWorldPos.x < playerPos.x && transform.localScale.x > 0)
        {
            Flip();
        }
        else if (mouseWorldPos.x > playerPos.x && transform.localScale.x < 0)
        {
            Flip();
        }
    }
    void Flip()
    {
        facingDirection *= -1;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }

    void FixedUpdate()
    {
        if (isDead) return;

        rb.velocity = moveInput * speed;
    }

    public void Die()
    {
        isDead = true;
        rb.velocity = Vector2.zero;

        if (anim != null)
        {
            anim.SetBool("isDead", true);
            anim.SetBool("isWalking", false);
        }
        Debug.Log("💀 Player đã chết");

    }
}
