using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace MasterScripts
{
    public class TileChunk
    {
        private int _width;
        private int _height;
        private int _widthOffset;
        private int _heightOffset;
        private int _chunkOffsetWidth;
        private int _chunkOffsetHeight;

        int[,] _tileData;
        GameObject[,] _tileGameObject;

        public TileChunk(
            int width,
            int height,
            int chunkOffsetWidth,
            int chunkOffsetHeight,
            bool placeCenterSource
        ) : this(width, height, chunkOffsetWidth, chunkOffsetHeight)
        {
            // Seed / Source Block
            this._tileData[_width + _widthOffset, _height + _heightOffset] = 2;
            this._tileData[_width + _widthOffset - 1, _height + _heightOffset] = 2;
            this._tileData[_width + _widthOffset, _height + _heightOffset - 1] = 2;
            this._tileData[_width + _widthOffset - 1, _height + _heightOffset - 1] = 2;

            // Endpoints
            SetValue(new Vector3Int(-10, -5, 0), 3);
            SetValue(new Vector3Int(-8, 2, 0), 3);
            SetValue(new Vector3Int(8, -5, 0), 3);
            SetValue(new Vector3Int(9, 2, 0), 3);
        }


        public TileChunk(
            int width,
            int height,
            int chunkOffsetWidth,
            int chunkOffsetHeight
        )
        {
            this._width = width;
            this._height = height;
            this._widthOffset = -width / 2;
            this._heightOffset = -height / 2;
            this._chunkOffsetWidth = chunkOffsetWidth;
            this._chunkOffsetHeight = chunkOffsetHeight;

            this._tileData = new int[width, height];
            this._tileGameObject = new GameObject[width, height];
        }

        private Vector3Int GetPosition(int x, int y)
        {
            return new Vector3Int(
                x + _widthOffset + _chunkOffsetWidth * _widthOffset,
                y + _heightOffset + _chunkOffsetHeight * _heightOffset,
                0
            );
        }

        private int[] GetIndex(Vector3Int position)
        {
            int[] result = new int[2]
            {
                position[0] - _widthOffset + _chunkOffsetWidth * _widthOffset,
                position[1] - _heightOffset + _chunkOffsetHeight * _heightOffset
            };
            return result;
            ;
        }

        public GameObject GetGameObject(Vector3Int position)
        {
            int[] index = GetIndex(position);
            return _tileGameObject[index[0], index[1]];
        }

        public void SetGameObject(Vector3Int position, GameObject value)
        {
            int[] index = GetIndex(position);
            _tileGameObject[index[0], index[1]] = value;
        }

        public void SetValue(Vector3Int position, int value)
        {
            int[] index = GetIndex(position);
            _tileData[index[0], index[1]] = value;
        }

        public int GetValue(Vector3Int position)
        {
            int[] index = GetIndex(position);
            return _tileData[index[0], index[1]];
        }

        public void RunUpdate()
        {
            for (int i = 0; i < _width; i++)
            for (int j = 0; j < _height; j++)
                if (_tileData[i, j] == 1)
                    // TODO: implement queue to avoid needless SetTile calls
                    TileManager.Instance.collisionMap.SetTile(
                        GetPosition(i, j), TileManager.Instance.rootTile);
                else if (_tileData[i, j] == 2)
                    TileManager.Instance.collisionMap.SetTile(
                        GetPosition(i, j), TileManager.Instance.sourceTile);
                else if (_tileData[i, j] == 3)
                    TileManager.Instance.collisionMap.SetTile(
                        GetPosition(i, j), TileManager.Instance.endpointTile);
                else
                    TileManager.Instance.collisionMap.SetTile(
                        GetPosition(i, j), null);
        }

        private bool[,] visited;

        void DFS(int x, int y)
        {
            visited[x, y] = true;
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    if (i == 0 && j == 0) continue;
                    int nx = x + i;
                    int ny = y + j;
                    if (nx >= 0 && nx < _width && ny >= 0 && ny < _height && _tileData[nx, ny] == 1 && !visited[nx, ny])
                    {
                        DFS(nx, ny);
                    }
                }
            }
        }

        public List<Vector3Int> GetUnconnectedTiles()
        {
            List<Vector3Int> results = new List<Vector3Int>();
            visited = new bool[_width, _height];
            for (int i = 0; i < _width; i++)
            {
                for (int j = 0; j < _height; j++)
                {
                    visited[i, j] = false;
                }
            }

            DFS(_width / 2, _height / 2);
            for (int i = 0; i < _width; i++)
            {
                for (int j = 0; j < _height; j++)
                {
                    if (!visited[i, j] && _tileData[i, j] > 0)
                    {
                        results.Add(GetPosition(i, j));
                    }
                }
            }   

            return results;
        }

        int GetItemCount(int item)
        {
            int count = 0;
            for (int i = 0; i < _width; i++)
            {
                for (int j = 0; j < _height; j++)
                {
                    if (_tileData[i, j] == item)
                    {
                        count++;
                    }
                }
            }

            return count;
        }
    }
}