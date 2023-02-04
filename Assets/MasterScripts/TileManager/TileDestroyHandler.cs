using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TileDestroyHandler : MonoBehaviour
{

    [SerializeField]
    public float attackDuration = 2f;
    private float lastAttacked = 0;

    public void AttackStarted()
    {
        Debug.Log($"Attack started! {Time.time}");
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
        Destroy(gameObject);
        // check all the neigbhours in another direciton of seed
    }

}
