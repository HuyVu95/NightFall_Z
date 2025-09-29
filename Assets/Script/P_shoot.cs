using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_shoot : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireRate = 0.3f;
    private float nexFireTime = 0f;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(0) && Time.time >= nexFireTime)
        {
            Shoot();
            nexFireTime = Time.time + fireRate;
        }
    }
    void Shoot()
    {
        Vector3 mouseWorldPos3D = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mouseWorldPos = new Vector2(mouseWorldPos3D.x, mouseWorldPos3D.y);
        Vector2 firePos = firePoint.position;

        Vector2 direction = (mouseWorldPos - firePos).normalized;
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        
        Collider2D playerCollider = GetComponent<Collider2D>();
        Collider2D bulletCollider = bullet.GetComponent<Collider2D>();
        Physics2D.IgnoreCollision(bulletCollider, playerCollider);

        bullet.GetComponent<Bullet_P>().Initialize(direction);
        Debug.Log("Da Ban");
    }

}
