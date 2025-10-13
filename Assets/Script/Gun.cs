//using System.Collections;
//using UnityEngine;

//public class Gun : MonoBehaviour
//{
//    [Header("Bullet Settings")]
//    public GameObject bulletPrefab;
//    public Transform firePoint;
//    public float fireRate = 0.3f;

//    [Header("Ammo Settings")]
//    public int maxAmmo = 10;
//    public int reserveAmmo = 30;
//    public float reloadTime = 1.5f;

//    public int maxTotalAmmo => maxAmmo + reserveAmmo;


//    [HideInInspector] public int currentAmmo;
//    [HideInInspector] public bool isReloading = false;
//    private float nextFireTime = 0f;

//    void Awake()
//    {
//        currentAmmo = maxAmmo;
//    }

//    public bool CanShoot()
//    {
//        return !isReloading && Time.time >= nextFireTime && currentAmmo > 0;
//    }

//    public void Shoot()
//    {
//        if (!CanShoot()) return;

//        nextFireTime = Time.time + fireRate;
//        currentAmmo--;

//        // Xác định hướng chuột
//        Vector3 mouseWorldPos3D = Camera.main.ScreenToWorldPoint(Input.mousePosition);
//        Vector2 mouseWorldPos = new Vector2(mouseWorldPos3D.x, mouseWorldPos3D.y);
//        Vector2 firePos = firePoint.position;
//        Vector2 direction = (mouseWorldPos - firePos).normalized;

//        // Tạo đạn
//        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
//        Collider2D playerCollider = GetComponentInParent<Collider2D>();
//        Collider2D bulletCollider = bullet.GetComponent<Collider2D>();
//        Physics2D.IgnoreCollision(bulletCollider, playerCollider);

//        bullet.GetComponent<Bullet_P>().Initialize(direction);

//        Debug.Log($"[{name}] Bắn 1 viên — còn lại {currentAmmo}/{maxAmmo}");
//    }

//    public IEnumerator Reload()
//    {

//        if (isReloading) yield break;
//        if (currentAmmo == maxAmmo || reserveAmmo <= 0) yield break; // Không reload nếu không cần

//        isReloading = true;
//        Debug.Log($"[{name}] Đang nạp đạn...");

//        yield return new WaitForSeconds(reloadTime);

//        // Tính toán số viên cần nạp
//        int needed = maxAmmo - currentAmmo;
//        int load = Mathf.Min(needed, reserveAmmo); // Lấy ít nhất giữa cần và còn

//        currentAmmo += load;
//        reserveAmmo -= load;

//        isReloading = false;
//        Debug.Log($"[{name}] Đã nạp xong → {currentAmmo}/{maxAmmo} (Còn lại: {reserveAmmo})");
//    }
//}
using System.Collections;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [Header("Bullet Settings")]
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireRate = 0.3f;

    [Header("Ammo Settings")]
    public int maxAmmo = 10;          // Đạn trong băng
    public int reserveAmmo = 30;      // Đạn dự trữ
    public float reloadTime = 1.5f;   // Thời gian nạp

    public int maxTotalAmmo => maxAmmo + reserveAmmo;

    [HideInInspector] public int currentAmmo;
    [HideInInspector] public bool isReloading = false;
    private float nextFireTime = 0f;

    void Awake()
    {
        currentAmmo = maxAmmo;
    }

    public bool CanShoot()
    {
        return !isReloading && Time.time >= nextFireTime && currentAmmo > 0;
    }

    public void Shoot()
    {
        if (!CanShoot() || firePoint == null || bulletPrefab == null) return;

        nextFireTime = Time.time + fireRate;
        currentAmmo--;

        // Tính hướng bắn theo vị trí chuột
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = ((Vector2)mouseWorldPos - (Vector2)firePoint.position).normalized;

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Collider2D playerCollider = GetComponentInParent<Collider2D>();
        Collider2D bulletCollider = bullet.GetComponent<Collider2D>();
        Physics2D.IgnoreCollision(bulletCollider, playerCollider);

        bullet.GetComponent<Bullet_P>()?.Initialize(direction);
        Debug.Log($"[{name}] 🔫 Bắn — còn {currentAmmo}/{maxAmmo}");
    }

    public IEnumerator Reload()
    {
        if (isReloading || currentAmmo == maxAmmo || reserveAmmo <= 0)
            yield break;

        isReloading = true;
        Debug.Log($"[{name}] 🔄 Đang nạp đạn...");

        yield return new WaitForSeconds(reloadTime);

        int needed = maxAmmo - currentAmmo;
        int load = Mathf.Min(needed, reserveAmmo);

        currentAmmo += load;
        reserveAmmo -= load;

        isReloading = false;
        Debug.Log($"[{name}] ✅ Nạp xong: {currentAmmo}/{maxAmmo} (Còn: {reserveAmmo})");
    }
}
