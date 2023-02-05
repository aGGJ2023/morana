using MasterScripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPointHandler : MonoBehaviour
{
    private string TILE_TAG = "Tile";
    private bool connected = false;

    void Start()
    {
        
    }

    void Update()
    {
        Collider2D[] circleColliders = Physics2D.OverlapCircleAll(transform.position, 0.75f);
        int count = 0;
        foreach(var coll in circleColliders)
            if (coll.CompareTag(TILE_TAG))
                count++;
           
        if (!connected && count > 0)
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.green;
            GameManager.Instance.EndpointConnected();
            EnemyManager.Instance.DisableSpawnPoint(gameObject);
            connected = true;
        } else if (connected  && count == 0)
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.white;
            GameManager.Instance.EndpointDisconnected();
            EnemyManager.Instance.EnableSpawnPoint(gameObject);
            connected = false;
        }

    }
}
