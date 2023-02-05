using MasterScripts;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance;

    [SerializeField]
    private GameObject slowEnemy;
    [SerializeField]
    private GameObject fastEnemy;
    [SerializeField]
    private List<GameObject> spawnLocations;
    [SerializeField]
    private int MaxSlowEnemyCount = 10; // should be modified according to number of tiles
    [SerializeField]
    private int MaxFastEnemyCount = 5;
    [SerializeField]
    private float slowEnemySpawnDelay = 3f;

    private List<GameObject> targetedTiles = new List<GameObject>();
    private int slowEnemyCount = 0;
    private int fastEnemyCount = 0;
    private int burst = 2;
    private float lastSpawned = 0;
    

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void Update()
    {
        if (Time.time - lastSpawned > slowEnemySpawnDelay)
        {
            if (slowEnemyCount < MaxSlowEnemyCount)
            {
                for (int i = 0; i < burst; i++)
                {
                    SpawnEnemy(slowEnemy);
                }
                lastSpawned = Time.time;
            }
        }
    }   

    private void SpawnEnemy(GameObject enemy)
    {

        var index = Random.Range(0, spawnLocations.Count - 1);
        Instantiate(enemy, spawnLocations[index].transform.position, Quaternion.identity);
        slowEnemyCount++;
    }

    public void RemoveEnemy()
    => slowEnemyCount--;

    public bool ContainsTarget(GameObject target)
        => targetedTiles.Contains(target);

    public void AddTarget(GameObject target)
        => targetedTiles.Add(target);

    public void RemoveTarget(GameObject target)
        => targetedTiles.Remove(target);

    internal void DisableSpawnPoint(GameObject gameObject)
        => spawnLocations.Remove(gameObject);

    internal void EnableSpawnPoint(GameObject gameObject)
        => spawnLocations.Add(gameObject);
}
