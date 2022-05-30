using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Urxxxxx.GamePlay
{
    public class BaseSkill
    {
        public virtual void SetRangeSkill(Player player) { player.SetRangeSkill(this); }

        public virtual void ApplyRangeAttribute(RangeWeapon weapon) { }

        public virtual string GetSkillName()
        {
            return "";
        }
    }

}