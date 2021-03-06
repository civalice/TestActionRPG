using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Urxxxxx.GamePlay
{

    public class DummyBaseEnemy : BaseEnemy
    {
        public float MaxHP = 30;
        protected float CurrentHP;
        private Rigidbody rigidBody;

        public override void DamageTaken(float damage)
        {
            CurrentHP -= damage;
            if (CurrentHP < 0)
            {
                Death();
            }
        }

        public override void ApplyForce(Vector3 direction, float force)
        {
            rigidBody.AddForce(direction * force, ForceMode.Impulse);
        }
        protected override void Death()
        {
            base.Death();
        }

        void Awake()
        {
            rigidBody = GetComponent<Rigidbody>();
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
