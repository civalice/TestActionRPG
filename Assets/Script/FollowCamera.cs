using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
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
        public float MinZoom = 6;
        public float MaxZoom = 15;
        public float CurrentZoom = 13;
        private float targetZoom = 13;

        private bool inputMouseScrollUp;
        private bool inputMouseScrollDown;

        // Start is called before the first frame update
        void Start()
        {
            InitialDistance = transform.position - FollowGameObject.transform.position;
            NextLerp = transform.position;
        }

        // Update is called once per frame
        void Update()
        {
            Inputs();

            // Mouse zoom.
            if (inputMouseScrollUp)
            {
                targetZoom -= 0.1f;
                if (targetZoom < MinZoom) targetZoom = MinZoom;
            }
            else if (inputMouseScrollDown)
            {
                targetZoom += 0.1f;
                if (targetZoom > MaxZoom) targetZoom = MaxZoom;
            }

            CurrentZoom = Mathf.Lerp(CurrentZoom, targetZoom, Time.deltaTime * 3);
            Camera.main.orthographicSize = CurrentZoom;

            CurrentOffset = transform.position - FollowGameObject.transform.position - InitialDistance;
            if (CurrentOffset.sqrMagnitude > MaxOffset * MaxOffset)
            {
                CurrentOffset = CurrentOffset.normalized * MaxOffset * 0.7f;
                NextLerp = FollowGameObject.transform.position + InitialDistance + CurrentOffset;
            }
            transform.position = Vector3.Lerp(transform.position, NextLerp,
                Time.deltaTime * Speed);
        }

        private void Inputs()
        {
            inputMouseScrollUp = Mouse.current.scroll.ReadValue().y > 0f;
            inputMouseScrollDown = Mouse.current.scroll.ReadValue().y < 0f;

        }
    }
}