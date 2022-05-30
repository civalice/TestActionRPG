using System.Collections;
using System.Collections.Generic;
using RPGCharacterAnims;
using RPGCharacterAnims.Actions;
using RPGCharacterAnims.Lookups;
using UnityEngine;
using Urxxxxx.Util;

namespace Urxxxxx.GamePlay
{
    public class Player : MonoBehaviour, IHitBoxObject
    {
        RPGCharacterController rpgCharacterController;

        public float CurrentHp = 100;
        public int MaxHp => GameController.Instance.PlayerMaxHp;
        public RangeWeapon Weapon;

        public List<BaseSkill> Skills = new List<BaseSkill>();

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
            Weapon.SetBulletLayer(Layer.PlayerAttackBox);
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
            RandomSkill();
            Weapon.Reset();
            foreach (var skill in Skills)
            {
                Weapon.SetRangeSkill(skill);
            }
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

        public void SetRangeSkill(BaseSkill skill)
        {
            Weapon.SetRangeSkill(skill);
        }

        public void RandomSkill()
        {
            Skills.Clear();
            //get random template
            Skills.Add(new PlusAccuracySkill());
            Skills.Add(new PlusDamageSkill());
            Skills.Add(new PlusForceSkill());

            //loop discard until got 2
            while (Skills.Count > 2)
            {
                Skills.RemoveAt(Random.Range(0, Skills.Count));
            }
        }

        public string GetSkillListName()
        {
            string str = "";
            foreach (var skill in Skills)
            {
                str += skill.GetSkillName();
                str += "\n";
            }

            return str;
        }
    }
}