using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Urxxxxx.Util;

namespace Urxxxxx.GamePlay
{
    public class SimpleBullet : MonoBehaviour
    {
        public GameObject HitEffect;
        protected Ray BulletRay;
        protected Vector3 BulletStartPosition;
        protected Vector3 BulletDirection;
        protected bool IsBulletLaunch = false;
        public float BulletSpeed = 1;
        public float BulletRange = 10;
        public float BulletAccuracy = 100f;
        public float Damage = 1;
        public float Force = 0.1f;

        private Vector3 m_previousFramePosition;

        // Start is called before the first frame update
        void Start()
        {
        }

        public void SetBulletTarget(Vector3 startPosition, Vector3 targetPosition)
        {
            var randomVal = (100 - BulletAccuracy) / 100f;
            var randomVec = new Vector3(Random.Range(-randomVal, randomVal), Random.Range(-randomVal, randomVal), Random.Range(-randomVal, randomVal));

            transform.position = startPosition;
            BulletStartPosition = startPosition;
            BulletDirection = ((targetPosition - BulletStartPosition).normalized + randomVec).normalized;
            BulletRay = new Ray(BulletStartPosition, BulletDirection);
            IsBulletLaunch = true;
        }

        // Update is called once per frame
        void Update()
        {
            if (!IsBulletLaunch) return;
            m_previousFramePosition = transform.position;
            transform.position += BulletDirection * BulletSpeed * Time.deltaTime;
            //bool isHit = Physics.Raycast(BulletStartPosition, BulletDirection, BulletRange, Layer.EnemyHitBox);
            int layerMask = 1 << Layer.Walkable | CollisionSystem.GetHitBoxLayerMask(gameObject.layer);
            bool isHit = Physics.Raycast(BulletStartPosition, BulletDirection, out RaycastHit hit, BulletRange, layerMask);
            // If it hits something...
            if (isHit)
            {
                //calculate collider range
                if (IsBetweenPreviousFrame(hit.point) || IsPointInsideCollider(hit.collider))
                {
                    var randomVal = 0.02f;
                    var hitEffect = Instantiate(HitEffect, hit.point + new Vector3(Random.Range(-randomVal, randomVal), Random.Range(-randomVal, randomVal), Random.Range(-randomVal, randomVal)), Quaternion.identity);
                    Destroy(gameObject);

                    var enemy = hit.transform.GetComponent<BaseEnemy>();
                    if (enemy != null)
                    {
                        enemy.ApplyForce(BulletDirection, Force);
                        enemy.DamageTaken(Damage);
                    }
                }
            }

            if ((transform.position - BulletStartPosition).magnitude > BulletRange)
            {
                Destroy(gameObject);
            }
        }

        private bool IsBetweenPreviousFrame(Vector3 position)
        {
            var previousRange = (m_previousFramePosition - BulletStartPosition).magnitude;
            var currentRange = (transform.position - BulletStartPosition).magnitude;
            var testRange = (position - BulletStartPosition).magnitude;
            return previousRange <= testRange && testRange <= currentRange;
        }

        private bool IsPointInsideCollider(Collider collider)
        {
            return collider.bounds.Contains(m_previousFramePosition) || collider.bounds.Contains(transform.position);
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawLine(BulletStartPosition, BulletStartPosition + BulletDirection * 5);
            Handles.Label(BulletStartPosition, $"{BulletDirection}");
        }
    }
}