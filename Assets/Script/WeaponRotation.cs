using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRotation : MonoBehaviour
{
    public SpriteRenderer weaponSprite;
    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePos - transform.position;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
        
        if (angle > 90 || angle < -90)
        {
            transform.localScale = new Vector3(1, 1, -1); // lật Y
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1); // bình thường
        }



    }
    

}
