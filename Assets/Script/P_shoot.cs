//using UnityEngine;
//using System.Collections;
//using System.Collections.Generic;
//using TMPro;

//public class P_shoot : MonoBehaviour
//{
//    [Header("Weapon Control")]
//    public List<Gun> guns;           // Danh sách tất cả súng
//    public int currentGunIndex = 0;  // Súng hiện tại
//    public GameObject shootEffectPrefab;

//    private Gun currentGun;

//    [Header("UI Elements")]
//    public TMP_Text ammoText;        // Text hiển thị đạn
//    public TMP_Text gunNameText;    // Text hiển thị tên súng (tuỳ chọn)

//    void Start()
//    {
//        // Bật súng đầu tiên, tắt các súng còn lại
//        SetActiveGun(currentGunIndex);
//        UpdateAmmoUI();
//    }

//    void Update()
//    {
//        if (currentGun == null) return;

//        // --- Bắn ---
//        if (Input.GetMouseButton(0))
//        {
//            TryShoot();
//        }

//        // --- Nạp đạn ---
//        if (Input.GetKeyDown(KeyCode.R))
//        {
//            StartCoroutine(currentGun.Reload());
//        }

//        // --- Đổi súng ---
//        HandleWeaponSwitch();
//        // --- Cập nhật UI ---
//        UpdateAmmoUI();
//    }

//    void TryShoot()
//    {
//        if (currentGun.CanShoot())
//        {
//            currentGun.Shoot();

//            // Hiệu ứng bắn
//            if (shootEffectPrefab != null && currentGun.firePoint != null)
//            {
//                GameObject fx = Instantiate(shootEffectPrefab, currentGun.firePoint.position, currentGun.firePoint.rotation);
//                fx.transform.parent = currentGun.firePoint;
//                Destroy(fx, 0.1f);
//            }
//        }
//    }

//    void HandleWeaponSwitch()
//    {
//        // Nhấn phím 1, 2, 3... để đổi súng tương ứng trong danh sách
//        for (int i = 0; i < guns.Count; i++)
//        {
//            if (Input.GetKeyDown(KeyCode.Alpha1 + i))
//            {
//                SetActiveGun(i);
//                break;
//            }
//        }
//    }

//    public void SetActiveGun(int index)
//    {
//        if (index < 0 || index >= guns.Count) return;

//        // Tắt tất cả súng, bật súng được chọn
//        for (int i = 0; i < guns.Count; i++)
//        {
//            guns[i].gameObject.SetActive(i == index);
//        }

//        currentGunIndex = index;
//        currentGun = guns[currentGunIndex];

//        Debug.Log($"🔫 Đổi sang súng: {currentGun.name}");
//    }
//    void UpdateAmmoUI()
//    {
//        if (ammoText != null && currentGun != null)
//    {
//        ammoText.text = $"{currentGun.name} : {currentGun.currentAmmo} / {currentGun.maxTotalAmmo}";
//    }

//    if (gunNameText != null && currentGun != null)
//        gunNameText.text = $"{currentGun.name}";
//    }
//}
using UnityEngine;
using TMPro;
using System.Collections;

public class P_shoot : MonoBehaviour
{
    [Header("References")]
    public WeaponManager weaponManager;
    public GameObject shootEffectPrefab;

    [Header("UI Elements")]
    public TMP_Text ammoText;
    public TMP_Text gunNameText;

    private Gun currentGun;

    void Start()
    {
        currentGun = weaponManager.GetCurrentGun();
        UpdateAmmoUI();
    }

    void Update()
    {
        currentGun = weaponManager.GetCurrentGun();
        if (currentGun == null) return;

        // --- Bắn ---
        if (Input.GetMouseButton(0))
        {
            TryShoot();
        }

        // --- Nạp đạn ---
        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(currentGun.Reload());
        }

        // --- Đổi súng ---
        for (int i = 0; i < weaponManager.guns.Count; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i))
            {
                weaponManager.SwitchWeapon(i);
                break;
            }
        }

        UpdateAmmoUI();
    }

    void TryShoot()
    {
        if (currentGun.CanShoot())
        {
            currentGun.Shoot();

            if (shootEffectPrefab && currentGun.firePoint)
            {
                GameObject fx = Instantiate(shootEffectPrefab, currentGun.firePoint.position, currentGun.firePoint.rotation);
                fx.transform.SetParent(currentGun.firePoint);
                Destroy(fx, 0.15f);
            }
        }
    }

    void UpdateAmmoUI()
    {
        if (currentGun == null) return;

        if (ammoText != null)
            ammoText.text = $"{currentGun.name}: {currentGun.currentAmmo}/{currentGun.maxAmmo}";

        if (gunNameText != null)
            gunNameText.text = currentGun.name;
    }
}
