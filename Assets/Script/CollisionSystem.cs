using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Urxxxxx.Util;

namespace Urxxxxx.GamePlay
{
    public class CollisionSystem : MonoBehaviour
    {
        public static CollisionSystem Instance;

        void Awake()
        {
            if (Instance == null) Instance = this;
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public static int GetHitBoxLayerMask(int layer)
        {
            int layerMask = 1 << Layer.NeutralHitBox;

            if (layer == Layer.PlayerAttackBox || layer == Layer.NeutralAttackBox)
            {
                layerMask |= 1 << Layer.EnemyHitBox;
            }
            if (layer == Layer.EnemyAttackBox || layer == Layer.NeutralAttackBox)
            {
                layerMask |= 1 << Layer.PlayerHitBox;
            }

            return layerMask;
        }
    }
}
