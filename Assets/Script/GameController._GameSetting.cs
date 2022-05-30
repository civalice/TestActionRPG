using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Urxxxxx.GamePlay
{
    public partial class GameController
    {
        public int MaxSpawn => Setting?.MaxSpawn ?? 3;

        public int PlayerMaxHp => Setting?.PlayerMaxHp ?? 100;
        public int MeleeDamage => Setting?.MeleeDamage ?? 5;
        public float MeleeForce => Setting?.MeleeForce ?? 2;
        public float MeleeRange => Setting?.MeleeRange ?? 2;
    }
}