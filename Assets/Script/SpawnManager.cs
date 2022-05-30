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
        public List<GameObject> CurrentSpawnList = new List<GameObject>();
        private int additionalSpawn = 0;
        private double timer = 0;

        public bool IsStart = false;
            // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (!IsStart) return;
            if (SpawnList.Count <= 0) return;
            timer += Time.deltaTime;
            additionalSpawn = (int)(timer / 30f);
            if (CurrentSpawnList.Count < GameController.Instance.MaxSpawn + additionalSpawn)
            {
                //Spawn from SpawnList
                GameObject spawnPrefab = SpawnList.RandomItem();
                //Get spawn position
                var currentSpawn = Instantiate(spawnPrefab, GetSpawnPosition() + Vector3.up, Quaternion.identity);
                var enemy = currentSpawn.GetComponent<BaseEnemy>();
                enemy.TargetObject = Player;
                CurrentSpawnList.Add(currentSpawn);
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

        public void Reset()
        {
            Clear();
            additionalSpawn = 0;
            timer = 0;
        }

        public void StartSpawn()
        {
            IsStart = true;
        }

        public void Kill(GameObject obj)
        {
            if (CurrentSpawnList.Contains(obj))
            {
                CurrentSpawnList.Remove(obj);
                Destroy(obj);
            }
        }

        public void StopSpawn()
        {
            IsStart = false;
        }

        public void Clear()
        {
            foreach (var obj in CurrentSpawnList)
            {
                Destroy(obj);
            }
            CurrentSpawnList.Clear();
        }
    }
}