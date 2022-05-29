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

        private GameObject hitTarget = null;

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
                hitTarget = null;
                isHitEnable = false;
            }
        }

        public void Hit()
        {
            if (hitTarget != null)
            {
                AttackEnemy();
                hitTarget = null;
            }
            else
            {
                isHitEnable = true;
            }
        }

        private void OnTriggerEnter(Collider col)
        {
            Debug.Log($"Enter Trigger = true and {col.gameObject.name}/{col.gameObject.layer},{Layer.EnemyHitBox}");

            if (col.gameObject.layer == Layer.EnemyHitBox)
            {
                hitTarget = col.gameObject;
                if (isHitEnable)
                {
                    AttackEnemy();
                    hitTarget = null;
                }

                Debug.Log("Enter Trigger = true");
            }
        }

        private void OnTriggerExit(Collider col)
        {
            if (col.gameObject == hitTarget)
            {
                hitTarget = null;
            }
        }

        private void AttackEnemy()
        {
            Debug.LogWarning($"Attack Enemy : {hitTarget.name}");
        }
    }
}