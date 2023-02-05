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
        public TileChunk chunk = new(40, 40, 0, 0, true);
        public float maxDistance = 7f;
        public bool DeleteUnconnectedTiles;

        void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }

        private void Update()
        {
            chunk.RunUpdate();
        }

        public void RemoveTile(Vector3 tileGameObjectPosition)
        {
            Vector3Int cellPos = collisionMap.WorldToCell(tileGameObjectPosition);
            chunk.SetValue(cellPos, 0);

            // not worky very well
            if (DeleteUnconnectedTiles)
            {
                var tiles = chunk.GetUnconnectedTiles();

                foreach (var vector3Int in tiles)
                {
                    chunk.SetValue(vector3Int, 0);
                    var gameObject = chunk.GetGameObject(vector3Int);
                    chunk.SetGameObject(vector3Int, null);
                    EnemyManager.Instance.RemoveTarget(gameObject);
                    Destroy(gameObject);
                }
            }


            // Vector3 worldPosWithCorrectZ = new Vector3(cellPos.x + 0.5f, cellPos.y + 0.5f, 0f);
        }

        public void SetTile(Vector3Int position, int value, GameObject gameObject)
        {
            chunk.SetValue(position, value);
            chunk.SetGameObject(position, gameObject);
        }
    }
}