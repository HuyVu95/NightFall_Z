using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public WeaponManager weaponManager;
    public int ammo;
    public int gold;
    public int health = 100;

    public void AddAmmo(int amount)
    {
        ammo += amount;
        Debug.Log($"+{amount} đạn");
    }

    public void AddGold(int amount)
    {
        gold += amount;
        Debug.Log($"+{amount} vàng");
    }

    public void Heal(int amount)
    {
        health = Mathf.Min(health + amount, 100);
        Debug.Log($"+{amount} máu");
    }

}
