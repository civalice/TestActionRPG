using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Urxxxxx.GamePlay
{
    public abstract class BaseEnemy : MonoBehaviour, IHitBoxObject
    {
        public GameObject TargetObject;

        public virtual void DamageTaken(float damage) { }
        public virtual void ApplyForce(Vector3 direction, float force) { }

        protected virtual void Death()
        {
            Destroy(gameObject);
        }
    }
}