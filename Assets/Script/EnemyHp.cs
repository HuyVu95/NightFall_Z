using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class EnemyHp : MonoBehaviour
{
    
    public float maxHealth = 50f;
    private float currentHealth;
    public Image fillImage;
    public TextMeshProUGUI hpText;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        UpdateUI();
    }


    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateUI();
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    void UpdateUI()
    {
        float fillAmount = currentHealth / maxHealth;
        fillImage.fillAmount = fillAmount;
        hpText.text = Mathf.Ceil(currentHealth).ToString() + "/" + Mathf.Ceil(maxHealth);
    }

    protected virtual void Die()
    {
        Destroy(gameObject);
    }
}
