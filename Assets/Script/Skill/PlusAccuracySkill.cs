using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Urxxxxx.GamePlay
{
    public class PlusAccuracySkill : BaseSkill
    {
        public override void ApplyRangeAttribute(RangeWeapon weapon)
        {
            weapon.SetAccuracy(100);
        }

        public override string GetSkillName()
        {
            return "Plus Accuracy";
        }
    }
}
