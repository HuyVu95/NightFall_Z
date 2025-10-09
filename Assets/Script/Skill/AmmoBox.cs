using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoBox : MonoBehaviour
{
    private float refillRatio = 0.5f; // 50% mặc định

    public void Setup(float radius, float ratio)
    {
        refillRatio = ratio;
        // Nếu muốn dùng radius để tạo vùng ảnh hưởng, có thể thêm Collider2D với kích thước tương ứng
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("player") || other.CompareTag("Ally"))
        {
            var character = other.GetComponent<Character>();
            if (character != null)
            {
                character.RefillAmmo(refillRatio);
                Destroy(gameObject);
            }
        }
    }

}
