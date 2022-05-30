using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Urxxxxx.Util;

namespace Urxxxxx.GamePlay
{

    public class DummyRangeEnemy : BaseEnemy
    {
        public RangeWeapon Weapon;

        public float MaxHP = 50;
        protected float CurrentHP;

        public float DelayTiming = 5f;
        public float FireTiming = 2f;

        private float dTiming;
        private float fTiming;

        private bool isFiring = false;

        private Rigidbody rigidBody;

        public override void DamageTaken(float damage)
        {
            CurrentHP -= damage;
            Debug.Log(CurrentHP);
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
            dTiming = DelayTiming;
            fTiming = FireTiming;
            Weapon.SetBulletLayer(Layer.EnemyAttackBox);
        }

        // Update is called once per frame
        void Update()
        {
            if (!isFiring)
            {
                dTiming -= Time.deltaTime;
                if (dTiming <= 0)
                {
                    dTiming = DelayTiming;
                    isFiring = true;
                }
            }
            else
            {
                Weapon.SetTargetPosition(TargetObject.transform.position);
                Weapon.UpdateWeapon();
                fTiming -= Time.deltaTime;
                if (fTiming <= 0)
                {
                    fTiming = FireTiming;
                    isFiring = false;
                }
            }
        }
    }
}