using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.AI;
using Urxxxxx.Util;

namespace Urxxxxx.GamePlay
{
    public class SpawnManager : MonoBehaviour
    {
        public List<GameObject> SpawnList = new List<GameObject>();
        public GameObject Player;
        private GameObject currentSpawn;

            // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (SpawnList.Count <= 0) return;
            if (currentSpawn == null)
            {
                //Spawn from SpawnList
                GameObject spawnPrefab = SpawnList.RandomItem();
                //Get spawn position
                currentSpawn = Instantiate(spawnPrefab, GetSpawnPosition() + Vector3.up, Quaternion.identity);
                var enemy = currentSpawn.GetComponent<BaseEnemy>();
                enemy.TargetObject = Player;
            }
        }

        public Vector3 GetSpawnPosition()
        {
            NavMeshTriangulation triangulation = NavMesh.CalculateTriangulation();

            int vertexIndex = Random.Range(0, triangulation.vertices.Length);

            NavMeshHit hit;
            if (NavMesh.SamplePosition(triangulation.vertices[vertexIndex], out hit, 2f, NavMesh.AllAreas))
            {
                return hit.position;
            }

            return Vector3.zero;
        }
    }
}