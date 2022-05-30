using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Urxxxxx.GamePlay
{
    public class PlusDamageSkill : BaseSkill
    {
        public override void ApplyRangeAttribute(RangeWeapon weapon)
        {
            weapon.SetDamageMultiplier(2);
        }

        public override string GetSkillName()
        {
            return "Plus Damage";
        }
    }
}
