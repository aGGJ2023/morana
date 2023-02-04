using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace MasterScripts
{
    public class TileManager : MonoBehaviour
    {
        public static TileManager Instance;
        public Tilemap collisionMap;
        public Tile rootTile;
        public Tile sourceTile;
        public Tile endpointTile;
        public TileChunk chunk = new(18, 10, 0, 0, true);
        public float maxDistance = 7f;
        
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

        private void Update()
        {
            chunk.RunUpdate();
        }
    }
}