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
        public float FireRate = 0.1f;

        [Header("Default Projectile")] 
        public float BaseDamage = 1;
        public float BaseForce = 2;
        public float BaseBulletSpeed = 50;
        public float BaseBulletRange = 40;
        public float BaseAccuracy = 92;
    }
}