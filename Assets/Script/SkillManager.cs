using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public static SkillManager Instance;

    void Awake() => Instance = this;

    // public void ActivateSkill(SkillData skill, Character caster)
    // {
    //     if (skill.effectPrefab != null)
    //     {
    //         GameObject effect = Instantiate(skill.effectPrefab, caster.transform.position, Quaternion.identity);

    //         if (skill.isHealZone)
    //             effect.AddComponent<HealZone>().Setup(skill.range, skill.duration, 10); // 10 HP/s

    //         if (skill.isTurret)
    //             effect.AddComponent<Turret>().Setup(skill.range, skill.damage);

    //         if (skill.isAmmoBox)
    //             effect.AddComponent<AmmoBox>().Setup(skill.range, 0.5f); // refill 50%

    //         if (skill.isExplosive)
    //             effect.AddComponent<Explosion>().Setup(skill.range, skill.damage);

    //         if (skill.isHomingMissile)
    //             effect.AddComponent<HomingMissile>().Setup(skill.damage);

    //         if (skill.isStun)
    //             effect.AddComponent<StunEffect>().Setup(skill.range, skill.duration);

    //         if (skill.isMeleeCombo)
    //             effect.AddComponent<MeleeCombo>().Setup(3, skill.damage);
    //     }

    //     Debug.Log($"Kích hoạt kỹ năng: {skill.skillName}");
    // }

}
