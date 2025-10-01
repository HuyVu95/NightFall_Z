using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        Debug.Log("Nguoi choi bi tan cong! HP: " + currentHealth);
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    void Die()
    {
        Debug.Log("Nguoi choi da chet!");
        Destroy(gameObject);
    }
}
