using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shooter.Classic
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

        void OnGUI()
        {
            GUI.Label(new Rect(0, 0, 100, 100), _count.ToString());
        }

        void Start()
        {
            GM = this;
        }

        void Update()
        {
            if (Input.GetKeyDown("space"))
                AddShips(enemyShipIncremement);
        }

        void AddShips(int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                float xVal = Random.Range(leftBound, rightBound);
                float zVal = Random.Range(0f, 200f);

                Vector3 pos = new Vector3(xVal, 0f, zVal + topBound);
                Quaternion rot = Quaternion.Euler(0f, 180f, 0f);

                var obj = Instantiate(enemyShipPrefab, pos, rot) as GameObject;
            }
            _count += amount;
        }

    }
}

