using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

namespace Urxxxxx.GamePlay
{
    public class FollowCamera : MonoBehaviour
    {
        public GameObject FollowGameObject;

        public Vector3 InitialDistance;

        public float Speed = 1.5f;
        public float MaxOffset = 3f;
        public Vector3 CurrentOffset = Vector3.zero;
        public Vector3 NextLerp;

        // Start is called before the first frame update
        void Start()
        {
            InitialDistance = transform.position - FollowGameObject.transform.position;
            NextLerp = transform.position;
        }

        // Update is called once per frame
        void Update()
        {
            CurrentOffset = transform.position - FollowGameObject.transform.position - InitialDistance;
            if (CurrentOffset.sqrMagnitude > MaxOffset * MaxOffset)
            {
                CurrentOffset = CurrentOffset.normalized * MaxOffset * 0.7f;
                NextLerp = FollowGameObject.transform.position + InitialDistance + CurrentOffset;
            }
            transform.position = Vector3.Lerp(transform.position, NextLerp,
                Time.deltaTime * Speed);
        }
    }
}