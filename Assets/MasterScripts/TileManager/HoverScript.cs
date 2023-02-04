using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace MasterScripts
{
    public class HoverScript : MonoBehaviour
    {
        public Tilemap overlayTilemap;
        public TileBase tile;

        // While true, will only allow hovering next to existing tiles 
        public bool adhereToHoverRules;

        private Vector3Int _lastCellPos;

        void Update()
        {
            Vector3 mousePos = Input.mousePosition;
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
            Vector3Int cellPos = overlayTilemap.WorldToCell(worldPos);

            if (_lastCellPos == cellPos) return;
            overlayTilemap.SetTile(_lastCellPos, null);

            if (!adhereToHoverRules | CanHoverAt(cellPos))
            {
                overlayTilemap.SetTile(cellPos, tile);
                _lastCellPos = cellPos;
            }
        }

        private bool CanHoverAt(Vector3Int cellPos)
        {
            // returns false when current tile is occupied
            if (!TileManager.Instance.collisionMap.GetTile(cellPos).IsUnityNull())
                return false;

            // returns true if current tile has valid neighbors
            for (int x = -1; x < 2; x++)
            for (int y = -1; y < 2; y++)
                if (!TileManager.Instance.collisionMap.GetTile(new Vector3Int(
                        cellPos.x + x,
                        cellPos.y + y
                    )).IsUnityNull())
                    return true;

            return false;
        }
    }
}