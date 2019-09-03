using System.Collections;
using System.Collections.Generic;
using Unity.Jobs;
using UnityEngine;
using UnityEngine.Jobs;

namespace Shooter.JobSystem
{
    public class GameManager : MonoBehaviour
    {
        public int topBound;
        public int bottomBound;
        public int leftBound;
        public int rightBound;
        public int enemyShipIncremement;
        public float enemySpeed;
        public GameObject enemyShipPrefab;

        private int _count;

        public static GameManager GM { get; private set; }

        TransformAccessArray transforms;
        MovementJob moveJob;
        JobHandle moveHandle;

        void OnGUI()
        {
            GUI.Label(new Rect(0, 0, 100, 100), _count.ToString());
        }

        void OnDisable()
        {
            moveHandle.Complete();
            transforms.Dispose();
        }

        void Start()
        {
            GM = this;
            transforms = new TransformAccessArray(0, -1);
        }

        void Update()
        {
            moveHandle.Complete();

            if (Input.GetKeyDown("space"))
                AddShips(enemyShipIncremement);

            moveJob = new MovementJob()
            {
                moveSpeed = enemySpeed,
                topBound = topBound,
                bottomBound = bottomBound,
                deltaTime = Time.deltaTime
            };

            moveHandle = moveJob.Schedule(transforms);

            JobHandle.ScheduleBatchedJobs();
        }

        void AddShips(int amount)
        {
            moveHandle.Complete();

            transforms.capacity = transforms.length + amount;

            for (int i = 0; i < amount; i++)
            {
                float xVal = Random.Range(leftBound, rightBound);
                float zVal = Random.Range(0f, 10f);

                Vector3 pos = new Vector3(xVal, 0f, zVal + topBound);
                Quaternion rot = Quaternion.Euler(0f, 180f, 0f);

                var obj = Instantiate(enemyShipPrefab, pos, rot) as GameObject;

                transforms.Add(obj.transform);
            }

            _count += amount;
        }
    }
}

