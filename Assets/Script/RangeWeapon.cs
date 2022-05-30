using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Urxxxxx.Util;

namespace Urxxxxx.GamePlay
{
    public class RangeWeapon : MonoBehaviour
    {
        [SerializeField] public SimpleBullet BulletPrefab;

        public float FireRate = 0.1f;
        private float m_timing = 0;

        private int bulletLayer;
        public Vector3 TargetPosition;

        private bool m_hasTarget = false;

        // Start is called before the first frame update
        void Start()
        {

        }

        public void SetBulletLayer(int layer)
        {
            bulletLayer = layer;
        }

        public void SetTargetPosition(Vector3 targetPosition)
        {
            TargetPosition = targetPosition;
            m_hasTarget = true;
        }
        public void ResetTarget()
        {
            m_hasTarget = false;
        }

        public void UpdateWeapon()
        {
            if (!m_hasTarget || FireRate <= 0)
            {
                m_timing = 0;
                transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.identity, 10);
                return;
            }

            m_timing += Time.deltaTime;
            while (m_timing > FireRate)
            {
                m_timing -= FireRate;
                var bullet = Instantiate(BulletPrefab, transform.position, Quaternion.identity);
                var bulletComponent = bullet.GetComponent<SimpleBullet>();
                bullet.gameObject.layer = bulletLayer;
                bulletComponent.SetBulletTarget(transform.position, TargetPosition);
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}