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
        public int MeleeDamage = 5;

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
            if (hitTarget != null)
            {
                AttackEnemy();
            }
            else
            {
                isHitEnable = true;
            }
        }

        private void OnTriggerEnter(Collider col)
        {
            int layerTarget = CollisionSystem.GetHitBoxLayerMask(gameObject.layer);
            if ((1 << col.gameObject.layer & layerTarget) != 0)
            {
                hitTarget = col;
                if (isHitEnable)
                {
                    AttackEnemy();
                    hitTarget = null;
                }
            }
        }

        private void OnTriggerExit(Collider col)
        {
            if (col == hitTarget)
            {
                hitTarget = null;
            }
        }

        private void AttackEnemy()
        {
            var enemy = hitTarget.GetComponent<BaseEnemy>();
            if (enemy != null)
            {
                var randomVal = 0.02f;

                var hitEffect = Instantiate(HitPrefabs,
                    TriggerObject.transform.position + new Vector3(Random.Range(-randomVal, randomVal),
                        Random.Range(-randomVal, randomVal), Random.Range(-randomVal, randomVal)), Quaternion.identity);
                enemy.DamageTaken(MeleeDamage);
            }
        }
    }
}