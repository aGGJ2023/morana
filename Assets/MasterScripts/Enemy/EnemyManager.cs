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
    private List<Transform> spawnLocations;
    [SerializeField]
    private int MaxSlowEnemyCount = 10; // should be modified according to number of tiles
    [SerializeField]
    private int MaxFastEnemyCount = 5;

    private List<GameObject> targetedTiles = new List<GameObject>();
    private int slowEnemyCount = 0;
    private int fastEnemyCount = 0;

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
        if (slowEnemyCount < MaxSlowEnemyCount)
        {
            var index = Random.Range(0, spawnLocations.Count - 1);
            Instantiate(slowEnemy, spawnLocations[index].position, Quaternion.identity);
            slowEnemyCount++;
        }
    }

    public bool ContainsTarget(GameObject target)
        => targetedTiles.Contains(target);

    public void AddTarget(GameObject target)
        => targetedTiles.Add(target);

    public void RemoveTarget(GameObject target)
        => targetedTiles.Remove(target);
    

}
