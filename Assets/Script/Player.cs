using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Urxxxxx.GamePlay
{
    public class Player : MonoBehaviour
    {
        public RangeWeapon Weapon;

        public void SetTargetPosition(Vector3 target)
        {
            Vector3 targetVector = new Vector3(target.x, Weapon.transform.position.y, target.z);
            Weapon.SetTargetPosition(targetVector);
        }

        public void ResetTarget()
        {
            Weapon.ResetTarget();
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
    }
}