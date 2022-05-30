using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Urxxxxx.GamePlay
{
    public interface IHitBoxObject
    {
        public void DamageTaken(float damage);
        public void ApplyForce(Vector3 direction, float force);
    }
}