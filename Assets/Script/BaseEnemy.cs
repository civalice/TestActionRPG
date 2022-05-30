using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Urxxxxx.GamePlay
{
    public abstract class BaseEnemy : MonoBehaviour
    {
        public virtual void DamageTaken(float damage) { }
        protected virtual void Death()
        {
            Destroy(gameObject);
        }
    }
}