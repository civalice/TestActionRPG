using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Urxxxxx.Util
{
    [CreateAssetMenu(menuName = "Asset/GameSetting")]
    public class GameSetting : ScriptableObject
    {
        [Header("GamePlay")] 
        public int MaxSpawn = 3;

        [Header("Player")]
        public int PlayerMaxHp = 100;
        public int MeleeDamage = 5;
        public float MeleeForce = 2;
        public float MeleeRange = 2;
    }
}