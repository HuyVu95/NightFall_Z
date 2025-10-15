using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public float maxHP = 100f;
    public CharacterClassData characterClass;
    private float currentHP;
    private float currentAmmo;
    public float maxAmmo = 100f;


    void Start()
    {
        currentHP = 100 * (1 + characterClass.bonusHP);
        currentAmmo = 100 * (1 + characterClass.bonusAmmo);
        // Apply other bonuses as needed
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q)) UseSkill(characterClass.skill1);
        if (Input.GetKeyDown(KeyCode.E)) UseSkill(characterClass.skill2);
    }
    public void Heal(float amount)
    {
        currentHP += amount;
        currentHP = Mathf.Min(currentHP, maxHP); // không vượt quá HP tối đa
        Debug.Log($"{gameObject.name} hồi {amount} HP. HP hiện tại: {currentHP}");
    }
    public void RefillAmmo(float ratio)
    {
        float refillAmount = maxAmmo * ratio;
        currentAmmo += refillAmount;
        currentAmmo = Mathf.Min(currentAmmo, maxAmmo);
        Debug.Log($"{gameObject.name} được hồi {refillAmount} ammo. Ammo hiện tại: {currentAmmo}");
    }


    void UseSkill(SkillData skill)
    {
        SkillManager.Instance.ActivateSkill(skill, this);
    }

}
