using System;
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

        public TileChunk(
            int width,
            int height,
            int chunkOffsetWidth,
            int chunkOffsetHeight,
            bool placeCenterSource
        ) : this(width, height, chunkOffsetWidth, chunkOffsetHeight)
        {
            this._tileData[_width + _widthOffset, _height + _heightOffset] = 2;
            this._tileData[_width + _widthOffset - 1, _height + _heightOffset] = 2;
            this._tileData[_width + _widthOffset, _height + _heightOffset - 1] = 2;
            this._tileData[_width + _widthOffset - 1, _height + _heightOffset - 1] = 2;
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
        }
    }
}