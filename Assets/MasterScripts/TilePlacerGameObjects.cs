using MasterScripts;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilePlacerGameObjects : MonoBehaviour
{
    // While true, will only allow placing of tiles next to existing tiles 
    public bool adhereToPlaceRules;

    [SerializeField] GameObject TileObject;
    [SerializeField] Transform parent;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Tilemap collisionMap = TileManager.Instance.collisionMap;
            Vector3 mousePos = Input.mousePosition;
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
            Vector3Int cellPos = collisionMap.WorldToCell(worldPos);
            Vector3 worldPosWithCorrectZ = new Vector3(cellPos.x + 0.5f, cellPos.y + 0.5f, 0f);
            TileBase tile = collisionMap.GetTile(cellPos);
            
            if (!adhereToPlaceRules | CanPlaceAt(cellPos))
            {
                if (EnemyManager.Instance.TileAmount > 0)
                {
                    EnemyManager.Instance.TileAmount--;
                    Debug.Log(cellPos);
                    var gameObjectTile = Instantiate(TileObject, worldPosWithCorrectZ, Quaternion.identity, parent);
                    TileManager.Instance.SetTile(cellPos, 1, gameObjectTile);
                }
            }
        }
    }

    private bool CanPlaceAt(Vector3Int cellPos)
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
        for (int y = -1; y < 2; y++)
            if (!TileManager.Instance.collisionMap.GetTile(new Vector3Int(
                    cellPos.x + x,
                    cellPos.y + y
                )).IsUnityNull())
                return true;

        return false;
    }
}