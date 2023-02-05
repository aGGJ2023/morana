using System.Collections;
using System.Collections.Generic;
using MasterScripts;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Seed : MonoBehaviour
{
    [SerializeField]
    private int hitPoints = 10;

    public void TakeDamage()
    {
        Debug.Log("Seed is taking damage");
        hitPoints--;
        if (hitPoints <= 0)
            Die();
    }

    private void Die()
    {
        // TODO DEATH LOGIC
        EnemyManager.Instance.TileAmount = 0;
        GameManager.Instance.endpointsConnected = 0;
        Debug.Log("Death");
        SceneManager.LoadScene("TileScene");
    }
}
