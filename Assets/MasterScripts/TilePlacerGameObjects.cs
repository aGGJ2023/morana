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
            Vector3 worldPosWithCorrectZ = new Vector3(worldPos.x, worldPos.y, 0f);
            Vector3Int cellPos = collisionMap.WorldToCell(worldPos);
            TileBase tile = collisionMap.GetTile(cellPos);

            if (!adhereToPlaceRules | CanPlaceAt(cellPos))
            {
                TileManager.Instance.chunk.SetValue(cellPos, 1);
                Instantiate(TileObject, worldPosWithCorrectZ, Quaternion.identity, parent);
            }
                   

        }
    }

    private bool CanPlaceAt(Vector3Int cellPos)
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
