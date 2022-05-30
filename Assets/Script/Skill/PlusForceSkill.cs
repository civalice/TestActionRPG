using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Urxxxxx.GamePlay
{
    public class PlusForceSkill : BaseSkill
    {
        public override void ApplyRangeAttribute(RangeWeapon weapon)
        {
            weapon.AdditionalForce(5);
        }

        public override string GetSkillName()
        {
            return "Plus Force";
        }
    }
}