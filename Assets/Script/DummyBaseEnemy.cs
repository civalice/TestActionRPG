using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Urxxxxx.GamePlay
{

    public class DummyBaseEnemy : BaseEnemy
    {
        public float MaxHP = 30;
        protected float CurrentHP;

        public override void DamageTaken(float damage)
        {
            CurrentHP -= damage;
            if (CurrentHP < 0)
            {
                Death();
            }
        }

        protected override void Death()
        {
            base.Death();
        }

        // Start is called before the first frame update
        void Start()
        {
            CurrentHP = MaxHP;
        }

        // Update is called once per frame
        void Update()
        {
        }
    }

}
