using System.Collections;
using System.Collections.Generic;
using RPGCharacterAnims;
using UnityEngine;
using Urxxxxx.Util;

namespace Urxxxxx.GamePlay
{
    public class PlayerTriggerController : MonoBehaviour
    {
        RPGCharacterController rpgCharacterController;

        private RPGCharacterAnimatorEvents characterEvents;

        private bool isHitEnable = false;

        private Collider hitTarget = null;

        public GameObject TriggerObject;
        public GameObject HitPrefabs;
        public int MeleeDamage => GameController.Instance.MeleeDamage;
        public float HitForce => GameController.Instance.MeleeForce;
        public float HitRange => GameController.Instance.MeleeRange;

        void Awake()
        {
            rpgCharacterController = GetComponent<RPGCharacterController>();
        }

        // Start is called before the first frame update
        void Start()
        {
            characterEvents = rpgCharacterController.GetAnimatorTarget().GetComponent<RPGCharacterAnimatorEvents>();
            characterEvents.OnHit.AddListener(Hit);
        }

        // Update is called once per frame
        void Update()
        {
            if (rpgCharacterController.canAction)
            {
                isHitEnable = false;
            }
        }

        public void Hit()
        {
            bool isHit = Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, HitRange, CollisionSystem.GetHitBoxLayerMask(Layer.PlayerAttackBox));

            //Physics.BoxCast(transform.position, transform.localScale, transform.forward, out RaycastHit hit,
            //    transform.rotation, HitRange, CollisionSystem.GetHitBoxLayerMask(Layer.PlayerAttackBox));
            if (hit.collider != null)
            {
                hitTarget = hit.collider;
                AttackEnemy();
            }
            else
            {
                isHitEnable = true;
            }
        }

        private void AttackEnemy()
        {
            var enemy = hitTarget.GetComponent<IHitBoxObject>();
            if (enemy != null)
            {
                var randomVal = 0.02f;

                var hitEffect = Instantiate(HitPrefabs,
                    TriggerObject.transform.position + new Vector3(Random.Range(-randomVal, randomVal),
                        Random.Range(-randomVal, randomVal), Random.Range(-randomVal, randomVal)), Quaternion.identity);
                enemy.DamageTaken(MeleeDamage);
                enemy.ApplyForce(transform.forward, HitForce);
            }
        }
    }
}