using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Health : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;

    public Image hpBar;
    public TextMeshProUGUI hpText;

    public enum Type { Player, Enemy }
    public Type characterType;
    public bool IsDead => currentHealth <= 0;

    void Start()
    {
        currentHealth = maxHealth;
        UpdateUI();
    }

    public void TakeDamage(float damage)
    {
        if (IsDead) return;
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateUI();


        if (currentHealth <= 0)
        {
            if (characterType == Type.Player)
                Debug.Log("Player chết");
            else
            {
                //Destroy(gameObject);
                Debug.Log($"{gameObject.name} máu = 0 xử lý animation die.");
                SendMessage("OnDeath", SendMessageOptions.DontRequireReceiver);

            }
                
        }
    }


    void UpdateUI()
    {
        if (hpBar != null)
            hpBar.fillAmount = currentHealth / maxHealth;

        if (hpText != null)
            hpText.text = Mathf.Ceil(currentHealth) + "/" + Mathf.Ceil(maxHealth);
    }
    
}
