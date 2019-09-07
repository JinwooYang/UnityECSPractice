using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Transforms;
using UnityEngine;
using UnityEngine.Jobs;

namespace Shooter.ECS
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

        private EntityManager _entityManager;

        void OnGUI()
        {
            GUI.Label(new Rect(0, 0, 100, 100), _count.ToString());
        }

        void Start()
        {
            GM = this;

            _entityManager = World.Active.EntityManager;
        }

        void Update()
        {
            if (Input.GetKeyDown("space"))
            {
                AddShips(enemyShipIncremement);
            }
        }

        void AddShips(int amount)
        {
            Entity prefab = GameObjectConversionUtility.ConvertGameObjectHierarchy(enemyShipPrefab, World.Active);

            var entities = new NativeArray<Entity>(amount, Allocator.Temp);
            _entityManager.Instantiate(prefab, entities);

            for (var i = 0; i < amount; ++i)
            {
                var xVal = Random.Range(leftBound, rightBound);
                var zVal = Random.Range(0f, 10f);

                _entityManager.SetComponentData(entities[i], new Translation { Value = new Unity.Mathematics.float3(xVal, 0f, topBound + zVal) });
                _entityManager.SetComponentData(entities[i], new Rotation { Value = new Unity.Mathematics.quaternion(0, 1, 0, 0) });
                _entityManager.AddComponentData(entities[i], new MoveSpeed { Value = enemySpeed });
            }

            entities.Dispose();

            _count += amount;
        }
    }
}

