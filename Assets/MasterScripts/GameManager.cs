using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

namespace MasterScripts
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;
        public GameObject Seed;
        public GameObject player;
        public TextMeshProUGUI text;

        [SerializeField]
        private int endpointsRequired = 4;

        public int endpointsConnected = 0;

        void Awake()
        {
            if (Instance != null) 
            {
                Destroy(gameObject);
                
                return;
            }

            Instance = this;
          //  DontDestroyOnLoad(gameObject);
        }

        public void EndpointConnected()
        {
            endpointsConnected++;
            Debug.Log($"endpointsConnected: {endpointsConnected}");
            if (endpointsConnected == endpointsRequired)
            {
                Debug.Log("Win");
                // TODO LOAD WIN SCENE
                
                EnemyManager.Instance.TileAmount = 0;
                endpointsConnected = 0;
                SceneManager.LoadScene("MainScene");

            }
        }
        public void EndpointDisconnected()
        =>  endpointsConnected--;


    }
}