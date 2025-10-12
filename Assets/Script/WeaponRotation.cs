using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRotation : MonoBehaviour
{
    [Header("References")]
    public Transform weaponPivot;   // Gốc xoay
    public Transform weaponVisual;  // Con chứa sprite + firepoint
    public SpriteRenderer weaponSprite;

    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePos - weaponPivot.position;
        direction.z = 0f;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // ✅ Xoay pivot theo chuột
        weaponPivot.rotation = Quaternion.Euler(0f, 0f, angle);

        // ✅ Lật phần hiển thị, không lật pivot
        bool isBehind = direction.x < 0f;
        weaponVisual.localScale = new Vector3(isBehind ? -1f : 1f, isBehind ? -1f : 1f, 1f);
       

    }
}
