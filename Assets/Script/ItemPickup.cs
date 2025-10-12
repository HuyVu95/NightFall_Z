using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static itemType;

public class ItemPickup : MonoBehaviour
{
    public ItemType itemType;
    public int value = 10; // số lượng vàng, đạn, máu
    public Weapon weaponData; // chỉ dùng nếu là HeavyWeapon

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("player")) return;

        PlayerInventory inventory = other.GetComponent<PlayerInventory>();
        if (inventory == null) return;

        switch (itemType)
        {
            case ItemType.Ammo:
                inventory.AddAmmo(value);
                break;
            case ItemType.Gold:
                inventory.AddGold(value);

                break;
            case ItemType.Health:
                inventory.Heal(value);
                break;
            case ItemType.HeavyWeapon:
                if (weaponData != null && weaponData.weaponName.Contains("Heavy"))
                {
                    inventory.weaponManager.AddWeapon(weaponData);
                }
                break;
        }

        Destroy(gameObject); // xóa item sau khi nhặt
    }

}
