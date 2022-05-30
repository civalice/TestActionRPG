using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Urxxxxx.GamePlay
{
    public class DummyFollowEnemy : BaseEnemy
    {
        public NavMeshAgent Agent;

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

        public virtual void Awake()
        {
            rigidBody = GetComponent<Rigidbody>();
            Agent = GetComponent<NavMeshAgent>();
            CurrentHP = MaxHP;
        }
        // Start is called before the first frame update
        public virtual void Start()
        {

        }

        // Update is called once per frame
        public virtual void Update()
        {
            if (TargetObject != null)
                Agent.destination = TargetObject.transform.position;
        }
    }
}