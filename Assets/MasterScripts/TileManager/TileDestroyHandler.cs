using System.Collections;
using System.Collections.Generic;
using MasterScripts;
using UnityEditor;
using UnityEngine;

public class TileDestroyHandler : MonoBehaviour
{

    [SerializeField]
    public float attackDuration = 2f;
    private float lastAttacked = 0;

    public void AttackStarted()
    {
        lastAttacked = Time.time;
    }

    public void AttackStopped()
    {
        lastAttacked = 0;
    }

    public void Update()
    {
        if (lastAttacked != 0 && Time.time - lastAttacked > attackDuration) 
            DestroyTile();
    }

    public void DestroyTile()
    {
        EnemyManager.Instance.RemoveTarget(gameObject);
        TileManager.Instance.RemoveTile(gameObject.transform.position);
        Destroy(gameObject);
        // check all the neigbhours in another direciton of seed
        // no i don't think i will
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Physics2D.IgnoreCollision(collision.collider, collision.otherCollider);
            collision.gameObject.GetComponent<PlayerController>().GiveSpeedModifier();
        }
    }

}
