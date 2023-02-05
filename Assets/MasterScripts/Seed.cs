using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seed : MonoBehaviour
{
    [SerializeField]
    private int hitPoints = 15;

    public void TakeDamage()
    {
        hitPoints--;
        if (hitPoints <= 0)
            Die();
    }

    private void Die()
    {
        // TODO DEATH LOGIC
        Debug.Log("Death");
    }
}
