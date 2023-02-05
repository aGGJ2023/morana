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
            else
            {
                _lastCellPos = new Vector3Int(0, 0, -1);
            }
        }

        private bool CanHoverAt(Vector3Int cellPos)
        {
            // returns false when current tile is occupied
            if (!TileManager.Instance.collisionMap.GetTile(cellPos).IsUnityNull())
                return false;

            Vector3 tilePosition = TileManager.Instance.collisionMap.CellToWorld(cellPos);
            Vector3 tileCenter = new Vector3(tilePosition.x + 0.5f, tilePosition.y + 0.5f, tilePosition.z);
            Vector3 playerPosition = GameManager.Instance.player.transform.position;

            // returns false when the player is too far away
            if (Vector3.Distance(tileCenter, playerPosition)
                > TileManager.Instance.maxDistance)
                return false;


            // returns true if current tile has valid neighbors
            for (int x = -1; x < 2; x++)
            {
                for (int y = -1; y < 2; y++)
                {
                    var val = TileManager.Instance.GetTile(new Vector3Int(
                        cellPos.x + x,
                        cellPos.y + y
                    ));
                    if (val == 1 || val == 2)
                        return true;
                }
            }

            return false;
        }
    }
}