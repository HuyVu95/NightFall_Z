using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCharacterClass", menuName = "Character/Class")]

public class CharacterClassData : ScriptableObject
{
    public string className;
    public Sprite classIcon;

    public float bonusHP;
    public float bonusDamage;
    public float bonusSpeed;
    public float bonusAmmo;

    public SkillData skill1;
    public SkillData skill2;

}
