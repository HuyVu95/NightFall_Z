using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public CharacterClassData characterClass;
    private float currentHP;
    private float currentAmmo;

    void Start()
    {
        currentHP = 100 * (1 + characterClass.bonusHP);
        currentAmmo = 100 * (1 + characterClass.bonusAmmo);
        // Apply other bonuses as needed
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) UseSkill(characterClass.skill1);
        if (Input.GetKeyDown(KeyCode.Alpha2)) UseSkill(characterClass.skill2);
    }

    void UseSkill(SkillData skill)
    {
        SkillManager.Instance.ActivateSkill(skill, this);
    }

}
