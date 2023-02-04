using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace MasterScripts
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;
        public GameObject Seed;
        public GameObject player;

        void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        public Vector3 GetSeedLocation()
        {
            return Seed.transform.position;
        }

    }
}