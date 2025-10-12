using UnityEngine;
using System.Collections.Generic;
using System;

public class WeaponManager : MonoBehaviour
{
    [Header("Weapon Inventory")]
    public List<Gun> guns = new List<Gun>();
    public int currentGunIndex = 0;

    [Header("Weapon Holder")]
    public Transform weaponHolder;

    private Gun currentGun;

    void Start()
    {
        EquipWeapon(currentGunIndex);
    }

    public void EquipWeapon(int index)
    {
        if (index < 0 || index >= guns.Count) return;

        for (int i = 0; i < guns.Count; i++)
        {
            if (guns[i] != null)
                guns[i].gameObject.SetActive(i == index);
        }

        currentGunIndex = index;
        currentGun = guns[currentGunIndex];

        Debug.Log($"🔫 Trang bị súng: {currentGun.name}");
    }

    public void SwitchWeapon(int index)
    {
        EquipWeapon(index);
    }

    public Gun GetCurrentGun()
    {
        return currentGun;
    }

    public void AddWeapon(Gun newGun)
    {
        if (!guns.Contains(newGun))
        {
            guns.Add(newGun);
            Debug.Log($"📦 Nhặt được súng mới: {newGun.name}");
        }
    }

    internal void AddWeapon(Weapon weaponData)
    {
        throw new NotImplementedException();
    }
}
