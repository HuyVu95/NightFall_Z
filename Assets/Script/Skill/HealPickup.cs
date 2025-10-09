using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealPickup : MonoBehaviour
{
    private int healAmount;

    public void Setup(int amount)
    {
        healAmount = amount;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ally") || other.CompareTag("player"))
        {
            var character = other.GetComponent<Character>();
            if (character != null)
            {
                character.Heal(healAmount);
                Destroy(gameObject); // ✅ biến mất sau khi nhặt
            }
        }
    }

}
