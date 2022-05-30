using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Urxxxxx.Util
{
    public static class Layer
    {
        public static readonly int Walkable = LayerMask.NameToLayer("Walkable");
        public static readonly int PlayerAttackBox = LayerMask.NameToLayer("PlayerAttackBox");
        public static readonly int EnemyAttackBox = LayerMask.NameToLayer("EnemyAttackBox");
        public static readonly int NeutralAttackBox = LayerMask.NameToLayer("NeutralAttackBox");
        public static readonly int PlayerHitBox = LayerMask.NameToLayer("PlayerHitBox");
        public static readonly int EnemyHitBox = LayerMask.NameToLayer("EnemyHitBox");
        public static readonly int NeutralHitBox = LayerMask.NameToLayer("NeutralHitBox");

    }
}