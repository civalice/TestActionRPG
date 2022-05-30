using System.Collections;
using System.Collections.Generic;
using RPGCharacterAnims;
using RPGCharacterAnims.Actions;
using RPGCharacterAnims.Lookups;
using UnityEngine;

namespace Urxxxxx.GamePlay
{
    public class Player : MonoBehaviour, IHitBoxObject
    {
        RPGCharacterController rpgCharacterController;

        public float CurrentHp = 100;
        public int MaxHp => GameController.Instance.PlayerMaxHp;
        public RangeWeapon Weapon;

        public bool IsDead => CurrentHp <= 0;
        public void SetTargetPosition(Vector3 target)
        {
            Vector3 targetVector = new Vector3(target.x, Weapon.transform.position.y, target.z);
            Weapon.SetTargetPosition(targetVector);
        }

        public void ResetTarget()
        {
            Weapon.ResetTarget();
        }

        void Awake()
        {
            rpgCharacterController = GetComponent<RPGCharacterController>();
        }
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            Weapon.UpdateWeapon();
        }

        public void Reset()
        {
            CurrentHp = MaxHp;
            rpgCharacterController.TryEndAction(HandlerTypes.Death);
        }

        public virtual void DamageTaken(float damage)
        {
            CurrentHp -= damage;
            if (CurrentHp <= 0)
            {
                Death();
                return;
            }

            if (rpgCharacterController.HandlerExists(HandlerTypes.GetHit))
            {
                rpgCharacterController.StartAction(HandlerTypes.GetHit, new HitContext());
            }

        }

        public virtual void ApplyForce(Vector3 direction, float force)
        {

        }

        protected virtual void Death()
        {
            if (rpgCharacterController.HandlerExists(HandlerTypes.Death))
            {
                if (rpgCharacterController.CanStartAction(HandlerTypes.Death))
                {
                    rpgCharacterController.StartAction(HandlerTypes.Death);
                }
            }
        }
    }
}