using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "NewSkill", menuName = "Character/Skill")]

public class SkillData : ScriptableObject
{
    public string skillName;
    public string description;
    public float cooldown;
    public GameObject effectPrefab;

    public float duration;
    public float range;
    public int damage;

    public bool isHealPickup;
    public int healAmount;
    public bool isDebuffCleanse;
    public bool isTurret;
    public bool isAmmoBox;
    public bool isExplosive;
    public bool isHomingMissile;
    public bool isStun;
    public bool isMeleeCombo;

}
