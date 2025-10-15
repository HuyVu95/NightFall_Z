using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    public float zombieSpeed = 2f;
    private Transform player;
    [Header("Raycast Settings")]
    public float detectDistance = 1.5f; // khoảng cách dò vật cản
    public LayerMask obstacleMask;      // layer của vật cản (vd: "Ground" hoặc "vatCan")

    private Vector2 avoidDirection = Vector2.zero;
    private float avoidTime = 0f;
    private void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("player");
        if(playerObj != null)
        {
            player = playerObj.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //if(player != null)
        //{
        //    Vector2 direction = (player.position - transform.position).normalized;
        //    transform.position += (Vector3)(direction * zombieSpeed * Time.deltaTime);
        //}
        //if (CompareTag("vatCan"))
        //{

        //}
        if (player == null) return;

        Vector2 directionToPlayer = (player.position - transform.position).normalized;

        // Nếu đang né
        if (avoidTime > 0)
        {
            transform.position += (Vector3)(avoidDirection * zombieSpeed * Time.deltaTime);
            avoidTime -= Time.deltaTime;
            return;
        }

        // 🔍 Bắn tia Raycast về hướng đang đi
        RaycastHit2D hit = Physics2D.Raycast(transform.position, directionToPlayer, detectDistance, obstacleMask);
        Debug.DrawRay(transform.position, directionToPlayer * detectDistance, Color.red); // để thấy tia trong Scene

        if (hit.collider != null)
        {
            // Nếu gặp vật cản → chọn hướng né ngẫu nhiên
            float randomAngle = Random.Range(-90f, 90f);
            avoidDirection = Quaternion.Euler(0, 0, randomAngle) * directionToPlayer;
            avoidTime = 0.5f; // né trong 0.5 giây
            Debug.Log("⚠️ Vật cản phía trước! Né sang hướng khác");
        }
        else
        {
            // Không có vật cản → di chuyển thẳng về player
            transform.position += (Vector3)(directionToPlayer * zombieSpeed * Time.deltaTime);
        }
    }
}

