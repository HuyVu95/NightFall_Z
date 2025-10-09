using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRotation : MonoBehaviour
{
    //public SpriteRenderer weaponSprite;
    //public Transform weaponPivot;

    //void Update()
    //{
    //    Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //    Vector3 direction = mousePos - transform.position;


    //    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

    //    //weaponPivot.rotation = Quaternion.Euler(0f, 0f, angle);
    //    //// Nếu chuột nằm phía sau nhân vật (bên trái)
    //    //if (angle > 90 || angle < -90)
    //    //{
    //    //    weaponSprite.flipX = true;
    //    //    weaponSprite.flipY = true;  // Lật sprite
    //    //}
    //    //else
    //    //{
    //    //    weaponSprite.flipX = false;
    //    //    weaponSprite.flipY = false; // Bình thường
    //    //}

    //}
    public Transform weaponPivot; // chứa sprite + firepoint
    public SpriteRenderer weaponSprite;

    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePos - weaponPivot.position;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        weaponPivot.rotation = Quaternion.Euler(0f, 0f, angle);

        // Kiểm tra nếu chuột ở phía sau (góc > 90 hoặc < -90)
        if (angle > 90 || angle < -90)
        {
            // Lật toàn bộ vũ khí bằng trục X (không làm dốc)
            weaponPivot.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            weaponPivot.localScale = new Vector3(1, 1, 1);
        }
    }
}
